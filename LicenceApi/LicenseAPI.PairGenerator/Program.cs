using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseAPI.PairGenerator
{
    class Program
    {
        /*
         Pass    : Blue Byte Systems, Inc.
         Private : MHcwIwYKKoZIhvcNAQwBAzAVBBBicgsQh07gerhOiRUYUPnDAgEKBFB1vfxoYkihhwUDTZJ0UGJ86320WsTIVFyRLngD151KbN5kKffFTCMrGsRKo3kl3zvT1lZI+nLFMAxYwqzfJQsyqkEZwv4J3KKoIk6UeqqgeA==
         Public  : MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEwZq2xT4eBZQVgNATKNs8KwwCtscuv2QsiBiunMBZ/CRinlh7aluD1WfZodEHQdpv1oLTnBzLxDbL+I2dAqr/pQ==
         */

        static void Main(string[] args)
        {
            var passPhrase = "Blue Byte Systems, Inc.";
            var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
            var keyPair = keyGenerator.GenerateKeyPair();
            var privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
            var publicKey = keyPair.ToPublicKeyString();

            Console.WriteLine($"Pass    : {passPhrase}");
            Console.WriteLine($"Private : {privateKey}");
            Console.WriteLine($"Public  : {publicKey}");


            Console.Read();
        }
    }
}
