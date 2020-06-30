namespace Conexia.Domain.Shared.Facades
{
    public interface IEncryptionFacade
    {
        string Encrypt(string text);

        string Decrypt(string text);
    }
}
