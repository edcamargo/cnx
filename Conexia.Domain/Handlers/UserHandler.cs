using Conexia.Domain.Commands;
using Conexia.Domain.Dto;
using Conexia.Domain.Entities;
using Conexia.Domain.Handlers.Contracts;
using Conexia.Domain.Repositories;
using Conexia.Domain.Shared;
using Conexia.Domain.Shared.Contracts;
using Conexia.Domain.Shared.Facades;
using Flunt.Notifications;
using System.Collections.Generic;

namespace Conexia.Domain.Handlers
{
    public class UserHandler : Notifiable,
                               IHandler<AuthenticateCommand>,
                               IHandler<CreateUserCommand>,
                               IHandler<UpdateUserCommand>,
                               IHandler<DeleteUserCommand>,
                               IHandler<ResetPasswordCommand>,
                               IHandler<ForgotPasswordCommand>

    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionFacade _encryption;
        private readonly ITemperatureFacade _temperature;
        private readonly ISpotifyFacade _spotify;
        private readonly IEmailFacade _email;

        public UserHandler(IUserRepository repository,
                           IEncryptionFacade encryption,
                           ITemperatureFacade temperature,
                           ISpotifyFacade spotify,
                           IEmailFacade email)
        {
            _userRepository = repository;
            _encryption = encryption;
            _temperature = temperature;
            _spotify = spotify;
            _email = email;
        }

        public ICommandResult Handle(AuthenticateCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Não foi possível autenticar o usuário!", command.Notifications);

            var user = _userRepository.GetByEmail(command.Email);

            if (user == null)
                return new GenericCommandResult(false, "Não foi possível autenticar o usuário, e-mail não localizado!", command.Notifications);

            // Descriptografia da senha para validar o acesso
            var passwordEncrypt = _encryption.Decrypt(user.Password);

            // Compara senha do command de entrada com a senha do banco
            if (passwordEncrypt != command.Password)
                return new GenericCommandResult(false, "Não foi possível autenticar o usuário!", command.Notifications);

            var userDto = new UserDto(user.Id, user.Name, user.Email, user.City, user.PersonalNotes, 00.00);

            return new GenericCommandResult(true, "Usuário Autenticado", userDto);
        }

        public ICommandResult Handle(CreateUserCommand command)
        {
            // Fail Fast Validation
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Não foi possível criar o usuário!", command.Notifications);

            var valEmail = _userRepository.GetByEmail(command.Email);
            if(valEmail != null)
                return new GenericCommandResult(false, "Não foi possível criar o usuário, e-mail já existente!", command.Notifications);

            var searchTemperature = _temperature.GetTemperatureCity(command.City);
            if (searchTemperature == null)
                return new GenericCommandResult(false, "Não foi possível criar o usuário, verifique a cidade!", command.Notifications);

            // Criptografa a Password e Personal Notes
            var cryptUserPersonalNotes = _encryption.Encrypt(command.PersonalNotes);
            var cryptUserPassword = _encryption.Encrypt(command.Password);

            var user = new User(command.Name, command.Email, cryptUserPassword, command.City, cryptUserPersonalNotes);
            
            // Salva no banco
            _userRepository.Create(user);

            // Busca musicas para recomendar
            object playList = ReturnPlayListByTemperature(searchTemperature);

            // Transita apenas os dados necessários
            var userDto = new UserDto(user.Id, user.Name, user.Email, user.City, command.PersonalNotes, searchTemperature, playList);

            return new GenericCommandResult(true, "Usuário criado", userDto);
        }

        public ICommandResult Handle(UpdateUserCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Não foi possível atualizar o usuário!", command.Notifications);

            var user = _userRepository.GetById(command.Id);

            if (user != null)
            {
                var cryptUserPersonalNotes = _encryption.Encrypt(command.PersonalNotes);
                user.UpdateUser(command.Name, command.Email, command.City, cryptUserPersonalNotes);

                _userRepository.Update(user);
            }
            else return new GenericCommandResult(false, "Não foi possível atualizar o usuário, id inexistente!", command.Notifications);

            // Busca musicas para recomendar
            var searchTemperature = _temperature.GetTemperatureCity(user.City);
            object playList = ReturnPlayListByTemperature(searchTemperature);

            // Transita apenas os dados necessários
            var userDto = new UserDto(user.Id, user.Name, user.Email, user.City, user.PersonalNotes, playList);

            return new GenericCommandResult(true, "Usuário atualizado", userDto);
        }

        public ICommandResult Handle(DeleteUserCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Não foi possível deletar o usuário!", command.Notifications);

            var user = _userRepository.GetById(command.Id);
            if (user != null) 
                _userRepository.Delete(user);
            else 
                return new GenericCommandResult(false, "Não foi possível deletar o usuário, id inexistente!", command.Notifications);

            // Retorna o resultado
            return new GenericCommandResult(true, "Usuário excluído", command.Notifications);
        }

        public ICommandResult Handle(ResetPasswordCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Não foi possível atualizar o usuário!", command.Notifications);

            var user = _userRepository.GetByEmail(command.Email);
            if (user == null)
                return new GenericCommandResult(false, "Não foi possível alterar a senha, e-mail não existente!", command.Notifications);

            // Descriptografia da senha para validar
            var passwordDecrypt = _encryption.Decrypt(user.Password);

            // Compara senha do command de entrada com a senha do banco
            if (passwordDecrypt != command.Oldpassword)
                return new GenericCommandResult(false, "Não foi possível alterar a senha, antiga senha não confere!", command.Notifications);

            // Descriptografia da senha para validar o acesso
            var newPasswordEncrypt = _encryption.Encrypt(command.Password);
            user.UpdatePassword(newPasswordEncrypt);

            _userRepository.Update(user);

            return new GenericCommandResult(true, "Senha alterada com sucesso", null);
        }

        public ICommandResult Handle(ForgotPasswordCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Não foi possível lembrar a senha!", command.Notifications);

            var user = _userRepository.GetByEmail(command.Email);
            if (user == null)
                return new GenericCommandResult(false, "Não foi possível lembrar a senha, e-mail não existente!", command.Notifications);

            var passwordDecrypt = _encryption.Decrypt(user.Password);

            // Envio do email
            _email.Send("empresa@email.com.br", user.Email, "Solicitação de Senha", "sua senha é " + passwordDecrypt);

            IList<object> objReturnPassword = new List<object>()
            {
                new { nome = "FAKE: Senha enviada no email informado." , value = passwordDecrypt}
            };

            return new GenericCommandResult(true, "Senha recuperada com sucesso", objReturnPassword);
        }

        private object ReturnPlayListByTemperature(object searchTemperature)
        {
            // Busca musicas para recomendar
            object Musics;
            string genre = "classic";

            if ((double)searchTemperature > 30)
            {
                genre = "party";
                Musics = _spotify.GetPlayListByGenre(genre);
            }
            else if ((double)searchTemperature > 14 && (double)searchTemperature < 31)
            {
                genre = "pop";
                Musics = _spotify.GetPlayListByGenre(genre);
            }
            else if ((double)searchTemperature > 9 && (double)searchTemperature < 15)
            {
                genre = "rock";
                Musics = _spotify.GetPlayListByGenre(genre);
            }
            else Musics = _spotify.GetPlayListByGenre(genre);

            return new { genre, Musics };
        }
    }
}
