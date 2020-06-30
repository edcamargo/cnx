using Conexia.Domain.Commands;
using Conexia.Domain.Handlers;
using Conexia.InfraStructure.CrossCutting.Entities;
using Conexia.InfraStructure.CrossCutting.Security;
using Conexia.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Conexia.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public GenericCommandResult Autenticate([FromBody] AuthenticateCommand command, 
                                                [FromServices] TokenConfigurations tokenConfigurations,
                                                [FromServices] SigningConfigurations signingConfigurations,
                                                [FromServices] UserHandler handler)
        {
            var validateUser = (GenericCommandResult)handler.Handle(command);

            if (validateUser.Success)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(command.Email.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, command.Email)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = tokenHandler.WriteToken(securityToken);

                validateUser.Data = new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "Usuário Autenticado"
                };
            }

            return validateUser;
        }
    }
}
