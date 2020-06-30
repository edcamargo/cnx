using Conexia.Domain.Shared.Facades;

namespace Conexia.Tests.Repositories
{
    public class FakeEncryptionService : IEncryptionFacade
    {
        public string Decrypt(string text)
        {
            return "312dasddsads";
        }

        public string Encrypt(string text)
        {
            return "312dasddsads";
        }
    }
}
