using Conexia.Domain.Dto;
using System;
using System.Collections.Generic;

namespace Conexia.Domain.Queries
{
    public interface IUserQueries
    {
        UserDto GetById(Guid Id);

        IEnumerable<UserDto> GetAll();
    }
}
