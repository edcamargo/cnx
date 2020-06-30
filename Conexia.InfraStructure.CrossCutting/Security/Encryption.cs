using Conexia.Domain.Shared.Facades;
using Rijndael256;

namespace Conexia.InfraStructure.CrossCutting.Security
{
    public class Encryptions : IEncryptionFacade
    {
        private static readonly string _password = "edwin#the#best#developer";

        public string Encrypt(string text)
        {
            string textEncrypted = Rijndael.Encrypt(text, _password, KeySize.Aes256);

            return textEncrypted;
        }

        public string Decrypt(string text)
        {
            string textPlan = "";

            if (text != null)
            {
                textPlan = Rijndael.Decrypt(text, _password, KeySize.Aes256);
            }

            return textPlan;
        }
    }
}
