using Standard.Licensing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILicenceGenration
{
    public class LicenseManager
    {
        public string PrivateKey { get; set; }
        public string publicKey { get; set; }
        public string New(string CustomerName, string CustomerEmail, DateTime Expiry, string PrivateKey)
        {
            string passPhrase = CustomerName;

            var license = License.New().WithUniqueIdentifier(Guid.NewGuid()).As(LicenseType.Standard).ExpiresAt(Expiry)
     .LicensedTo(CustomerName, CustomerEmail)
    .CreateAndSignWithPrivateKey(PrivateKey, passPhrase);

            return license.ToString();


            //File.WriteAllText(licensePath, license.ToString(), Encoding.UTF8);
        }

        public string Newwithoutexpiry(string CustomerName, string CustomerEmail, string PrivateKey)
        {
            string passPhrase = CustomerName;

            var license = License.New().WithUniqueIdentifier(Guid.NewGuid()).As(LicenseType.Standard)
     .LicensedTo(CustomerName, CustomerEmail)
    .CreateAndSignWithPrivateKey(PrivateKey, passPhrase);

            return license.ToString();

        }

        public string LicencGenerate(string passPhrase)
        {
            var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
            var keyPair = keyGenerator.GenerateKeyPair();
            PrivateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
            publicKey = keyPair.ToPublicKeyString();
            
            return PrivateKey;
        }

    }
}
