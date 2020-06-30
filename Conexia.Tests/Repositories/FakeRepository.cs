using Conexia.Domain.Entities;
using Conexia.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Conexia.Tests.Repositories
{
    class FakeRepository : IUserRepository
    {
        public FakeRepository() { }

        public void Create(User todo)
        {
        }

        public void Update(User todo)
        {
        }

        public void Delete(User todo)
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            return new User("Edwin", "edwin.desenv@xxxxx.com", "123456", "Franca", "Teste Edwin");
        }

        public User Autenticate(string email, string password)
        {
            return new User("Edwin", "edwin.desenv@xxxxx.com", "123456", "Franca", "Teste Edwin");
        }

        public User GetByEmail(string email)
        {
            return new User("Edwin", "edwin.desenv@xxxxx.com", "123456", "Franca", "Teste Edwin");
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
