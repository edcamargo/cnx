using Conexia.Domain.Dto;
using Conexia.Domain.Repositories;
using Conexia.Domain.Shared.Facades;
using System;
using System.Collections.Generic;

namespace Conexia.Domain.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionFacade _encryption;

        private readonly ITemperatureFacade _temperature;
        private readonly ISpotifyFacade _spotify;

        public UserQueries(IUserRepository userRepository, 
                           IEncryptionFacade encryption,
                           ITemperatureFacade temperature,
                           ISpotifyFacade spotify)
        {
            _userRepository = userRepository;
            _encryption = encryption;
            _temperature = temperature;
            _spotify = spotify;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var user = _userRepository.GetAll();

            List<UserDto> userDtoList = new List<UserDto>();
            
            foreach (var item in user)
            {
                var searchTemperature = _temperature.GetTemperatureCity(item.City);
                object playList = ReturnPlayListByTemperature(searchTemperature);

                UserDto userDto = new UserDto(item.Id, 
                                              item.Name, 
                                              item.Email, 
                                              item.City,
                                              _encryption.Decrypt(item.PersonalNotes.ToString()), 
                                              searchTemperature,
                                              playList);
                userDtoList.Add(userDto);
            }

            return userDtoList;
        }

        public UserDto GetById(Guid Id)
        {
            var user = _userRepository.GetById(Id);

            var searchTemperature = _temperature.GetTemperatureCity(user.City);
            object playList = ReturnPlayListByTemperature(searchTemperature);

            UserDto userDto = new UserDto(user.Id, 
                                          user.Name, 
                                          user.Email, 
                                          user.City,
                                          _encryption.Decrypt(user.PersonalNotes.ToString()),
                                          searchTemperature,
                                          playList);
            return userDto;
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
