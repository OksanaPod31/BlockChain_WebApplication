

using ElectronicSignature.Certification;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Domain.Common.Utils
{
    public static class EDS
    {
        private static AsymmetricCipherKeyPair rsaKeyPair;
        public static void GenerateKeyRsa()
        {
            KeyGenerationParameters keyGenerationParameters = new KeyGenerationParameters(new SecureRandom(), 128);
            RsaKeyPairGenerator keyPairGenerator1 = new RsaKeyPairGenerator();
            keyPairGenerator1.Init(keyGenerationParameters);
            rsaKeyPair = keyPairGenerator1.GenerateKeyPair();

            using (var privateKeyWriter = new StreamWriter(@"rsaPrivateKey.pem"))
            {
                var pemWriter = new PemWriter(privateKeyWriter);
                pemWriter.WriteObject(rsaKeyPair.Private);
            }

            using (var publicKeyWriter = new StreamWriter(@"rsaPublicKey.pem"))
            {
                var pemWriter = new PemWriter(publicKeyWriter);
                pemWriter.WriteObject(rsaKeyPair.Public);
            }
        }

        public static string DecryptWithKey(string encoded)
        {
            
            var keyPair = rsaKeyPair;
            //var encoded = Cryptography.EncryptDataWithPublicKey(message, keyPair.Public);
            var data = Cryptography.DecryptDataWithPrivateKey(encoded, keyPair.Private);

            return data;
        }

        public static string EncryptWithKey(string message)
        {
            message = "Hello world";
            var keyPair = rsaKeyPair;
            var encoded = Cryptography.EncryptDataWithPublicKey(message, keyPair.Public);
           
            return encoded;
        }


    }
        
        
        //keyPairGenerator.Init(keyGenerationParameters);

        //var rsaKeyPair = keyPairGenerator.GenerateKeyPair();
    
}
