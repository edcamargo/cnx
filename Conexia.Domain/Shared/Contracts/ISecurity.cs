namespace Conexia.Domain.Shared.Contracts
{
    public interface ISecurity
    {
        string Encrypt(string textPlan);

        string Decrypt(string textEncrypted);
    }
}
