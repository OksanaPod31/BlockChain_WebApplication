
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using EllipticCurve;
using BlockchainApp.Domain.Common.Utils;

namespace BlockchainApp.Crypto.Signature
{
    public class SignatureService
    {
        public BigInteger SecretNumber { set; get; }
        public PrivateKey PrivKey { set; get; }
        public PublicKey PubKey { set; get; }

        public SignatureService(string screet = "")
        {
            if (screet != "")
            {
                PrivKey = new PrivateKey("secp256k1", BigInteger.Parse(screet));
            }
            else
            {
                PrivKey = new PrivateKey();
            }
            SecretNumber = PrivKey.secret;
            PubKey = PrivKey.publicKey();
        }

        public string GetPubKeyHex()
        {
            return Convert.ToHexString(PubKey.toString());
        }
        public string GetPrivKeyHex()
        {
            return Convert.ToHexString(PrivKey.toString());
        }

        public PublicKey GetPublicKeyFromHex(string publicKeyHex)
        {
            var byt = Converter.ConvertHexStringToByteArray(publicKeyHex);
            var publKey = PublicKey.fromString(byt);
            return publKey;
        }
        public PrivateKey GetPrivateKeyFromHex(string privateKeyHex)
        {
            var byt = Converter.ConvertHexStringToByteArray(privateKeyHex);
            var privKey = PrivateKey.fromString(byt);
            return privKey;
        }


        public string GetAddress()
        {
            byte[] hash = SHA256.Create().ComputeHash(PubKey.toString());
            return "UKC_" + Convert.ToBase64String(hash);
        }

        public string CreateSignature(string message)
        {
            EllipticCurve.Signature signature = Ecdsa.sign(message, PrivKey);
            return signature.toBase64();
        }
    }
}
