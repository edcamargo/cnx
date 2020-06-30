using Conexia.Domain.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conexia.Tests.CommandTests
{
    [TestClass]
    public class CreateUserCommandTests
    {
        private readonly CreateUserCommand _invalidCommand = new CreateUserCommand("", "", "", "", "");
        private readonly CreateUserCommand _validCommand = new CreateUserCommand("Edwin", "edwin.desenv@gmail.com", "123456", "Franca", "Teste Edwin");

        public CreateUserCommandTests()
        {
            _invalidCommand.Validate();
            _validCommand.Validate();
        }

        [TestMethod]
        public void Dado_um_comando_invalido()
        {
            Assert.AreEqual(_invalidCommand.Valid, false);
        }

        [TestMethod]
        public void Dado_um_comando_valido()
        {
            Assert.AreEqual(_validCommand.Valid, true);
        }
    }
}
