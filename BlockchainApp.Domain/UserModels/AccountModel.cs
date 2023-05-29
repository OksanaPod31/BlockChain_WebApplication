using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using EllipticCurve;

namespace BlockchainApp.Domain.UserModels
{
    public class AccountModel
    {
        public BigInteger SecretNumber { set; get; }
        public PrivateKey PrivKey { set; get; }
        public PublicKey PubKey { set; get; }

        public AccountModel(string screet = "")
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
