using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Agreement.Kdf;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using ECDiffieHellman_Bouncy_Castle.Entity;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ECDiffieHellman_Bouncy_Castle.Business
{
    public static class BouncyCastleKey
    {
        public static KeyObject NewKey()
        {
            KeyObject ret = new KeyObject();

            var gen = new ECKeyPairGenerator("ECDH");
            var secureRandom = new SecureRandom();
            var keyGenParam = new KeyGenerationParameters(secureRandom, ReadOnlyParam.keySize);

            gen.Init(keyGenParam);
            
            var keyPair = gen.GenerateKeyPair();

            ret.privateKey = exportKey(keyPair.Private);
            ret.publicKey = exportKey(keyPair.Public);

            return ret;
        }

        public static string exportKey(AsymmetricKeyParameter key)
        {
            string ret;

            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(key);
            pemWriter.Writer.Flush();

            ret = Regex.Replace(textWriter.ToString(), ReadOnlyParam.PemWritter.regex, "");

            return ret;
        }

        public static byte[] simetricKey(string pvtKey, string pubKey)
        {
            AsymmetricKeyParameter publicKey = (ECPublicKeyParameters)importKey(ReadOnlyParam.PemWritter.beginPublic+pubKey+ReadOnlyParam.PemWritter.endPublic);
            AsymmetricKeyParameter privateKey = ((AsymmetricCipherKeyPair)importKey(ReadOnlyParam.PemWritter.beginPrivate+pvtKey+ReadOnlyParam.PemWritter.endPrivate)).Private;

            ECDHCBasicAgreement acAgreement = new ECDHCBasicAgreement();
            acAgreement.Init(privateKey);
            BigInteger a = acAgreement.CalculateAgreement(publicKey);
            byte[] sharedSecret = a.ToByteArray();

            ECDHKekGenerator egH = new ECDHKekGenerator(DigestUtilities.GetDigest(ReadOnlyParam.sha));
            egH.Init(new DHKdfParameters(NistObjectIdentifiers.Aes, sharedSecret.Length, sharedSecret));
            byte[] symmetricKey = new byte[DigestUtilities.GetDigest(ReadOnlyParam.sha).GetDigestSize()];
            egH.GenerateBytes(symmetricKey, 0, symmetricKey.Length);

            return symmetricKey;
        }

        public static object importKey(string import)
        {
            using (var stringReader = new StringReader(import))
            {
                var pemReader = new PemReader(stringReader);
                var pemObject = pemReader.ReadObject();
                return pemObject;
            }
        }
    }
}