using Conexia.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Conexia.Tests.QueryTests
{
    [TestClass]
    public class UserQueriesTests
    {
        private List<User> _users;

        public UserQueriesTests()
        {
            _users = new List<User>();
            _users.Add(new User("Edwin", "edwin.desenv@gmail.com", "123456", "Franca", "Teste Edwin"));
            _users.Add(new User("Carlos", "carlos@gmail.com", "123456", "Ribeirão Preto", "Teste Edwin"));
            _users.Add(new User("Claudio", "claudio@gmail.com", "123456", "Franca", "Teste Edwin"));
            _users.Add(new User("Maria", "maria@gmail.com", "123456", "São Paulo", "Teste Edwin"));
        }

        [TestMethod]
        public void Deve_retornar_todos_usuarios()
        {
            var result = _users.AsQueryable();
            Assert.AreEqual(4, result.Count());
        }
    }
}
