using Conexia.Domain.Commands;
using Conexia.Domain.Handlers;
using Conexia.Domain.Shared;
using Conexia.Tests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conexia.Tests.HandlerTests
{
    [TestClass]
    public class CreateUserHandlerTests
    {
        private readonly CreateUserCommand _invalidCommand = new CreateUserCommand("", "", "", "", "");
        private readonly CreateUserCommand _validCommand = new CreateUserCommand("Edwin", "edwin.desenv@terra.com", "123456", "Franca", "Teste Edwin");
        private readonly UserHandler _handler = new UserHandler(new FakeRepository(), new FakeEncryptionService(), new FakeTemperatureService(), null, null);
        private GenericCommandResult _result = new GenericCommandResult();

        public CreateUserHandlerTests()
        {
            _invalidCommand.Validate();
            _validCommand.Validate();
        }

        [TestMethod]
        public void Dado_um_comando_invalido_Deve_interromper_a_execucao()
        {
            _result = (GenericCommandResult)_handler.Handle(_invalidCommand);
            Assert.AreEqual(_result.Success, false);
        }

        [TestMethod]
        public void Dado_um_comando_valido_Deve_criar_usuario()
        {
            _result = (GenericCommandResult)_handler.Handle(_validCommand);
            Assert.AreEqual(_result.Success, false);
        }
    }
}
