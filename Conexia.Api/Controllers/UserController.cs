using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Conexia.Domain.Commands;
using Conexia.Domain.Handlers;
using Conexia.Domain.Shared;
using Conexia.Domain.Dto;
using Conexia.Domain.Queries;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Conexia.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserQueries _userQueries;
        public UserController(IUserQueries userQueries)
            => _userQueries = userQueries;

        [HttpGet]
        public IEnumerable<UserDto> GetAll()
            => _userQueries.GetAll();

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public UserDto GetById(Guid id)
            => _userQueries.GetById(id);

        [Authorize("Bearer")]
        [HttpPost]
        public GenericCommandResult Create([FromBody] CreateUserCommand command, [FromServices] UserHandler handler)
            => (GenericCommandResult) handler.Handle(command);

        [Authorize("Bearer")]
        [HttpPut]
        public GenericCommandResult Update([FromBody] UpdateUserCommand command, [FromServices] UserHandler handler)
            => (GenericCommandResult)handler.Handle(command);

        [Authorize("Bearer")]
        [HttpDelete]
        public GenericCommandResult Delete([FromBody] DeleteUserCommand command, [FromServices] UserHandler handler)
            => (GenericCommandResult)handler.Handle(command);

        [HttpPost("ResetPassword")]
        public GenericCommandResult ResetPassword([FromBody] ResetPasswordCommand command, [FromServices] UserHandler handler) 
            => (GenericCommandResult) handler.Handle(command);

        [HttpPost("ForgotPassword")]
        public GenericCommandResult ForgotPassword([FromBody] ForgotPasswordCommand command, [FromServices] UserHandler handler)
            => (GenericCommandResult)handler.Handle(command);
    }
}
