using Conexia.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conexia.Tests.EntityTests
{
    [TestClass]
    public class UserTests
    {
        private readonly User _validPassword = new User("dasdsad", "edwin.desenv@gmail.com", "123456", "Franca", "Teste Edwin");

        [TestMethod]
        public void Dado_um_novo_user_o_mesmo_pode_ser_concluido()
        {
            Assert.AreEqual(_validPassword.UpdatePassword("1234567"), true);
        }
    }
}
