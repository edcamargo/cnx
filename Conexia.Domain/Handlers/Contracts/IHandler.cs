using Conexia.Domain.Shared.Contracts;

namespace Conexia.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
