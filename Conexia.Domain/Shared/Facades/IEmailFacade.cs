namespace Conexia.Domain.Shared.Facades
{
    public interface IEmailFacade
    {
        void Send(string to, string from, string subject, string body);
    }
}
