using Conexia.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Conexia.Domain.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);
        void Update(User user);
        void Delete(User user);
        User GetById(Guid id);
        User Autenticate(string email, string password);
        IEnumerable<User> GetAll();
        User GetByEmail(string email);
    }
}
