using System;
using Newtonsoft.Json;
using ECDiffieHellman_Bouncy_Castle.Business;
using ECDiffieHellman_Bouncy_Castle.Entity;

namespace ECDiffieHellman_Bouncy_Castle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("New Keys");
            Console.WriteLine(JsonConvert.SerializeObject(BouncyCastleKey.NewKey()));
            
            KeyObject Key1 = new KeyObject();
            Key1.publicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE2JLvbHUxcJd89STdRC3d7gxOzsljFoV/54xCcw+Ryxhdjmd/pSygAY1onHtg3TNgqR3t88sAIGntH++ZdizUwg==";
            Key1.privateKey = "MHcCAQEEICoJknGiV3SsXUV+Wr6FlapccpAoOfCr974bLJbN1aJDoAoGCCqGSM49AwEHoUQDQgAE2JLvbHUxcJd89STdRC3d7gxOzsljFoV/54xCcw+Ryxhdjmd/pSygAY1onHtg3TNgqR3t88sAIGntH++ZdizUwg==";
            Console.WriteLine("Key 1");
            Console.WriteLine(JsonConvert.SerializeObject(Key1));
            
            KeyObject Key2 = new KeyObject();
            Key2.publicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAExhucYVWbJdBtAi8Nf7QGBCe0KvhRSxPWkrPJbD9E+VAClahaJkuih+m4UUq5RXqycp1kFQ9qj+uuz8ppEU536g==";
            Key2.privateKey = "MHcCAQEEIJKkttPNNVqO3OD0XM0ieV9QnahApL4jYCVP4i2CRjuOoAoGCCqGSM49AwEHoUQDQgAExhucYVWbJdBtAi8Nf7QGBCe0KvhRSxPWkrPJbD9E+VAClahaJkuih+m4UUq5RXqycp1kFQ9qj+uuz8ppEU536g==";
            Console.WriteLine("Key 2");
            Console.WriteLine(JsonConvert.SerializeObject(Key2));

            byte[] simetricKey = BouncyCastleKey.simetricKey(Key1.privateKey, Key2.publicKey);
            Console.WriteLine("Simetric Key");
            Console.WriteLine(CommonBusiness.bytesToString(simetricKey));

            Console.WriteLine("Original Message:");
            string msg = "The dangers of not thinking clearly are much greater now than ever before. It's not that there's something new in our way of thinking - it's that credulous and confused thinking can be much more lethal in ways it was never before.";
            Console.WriteLine(msg);

            Console.WriteLine("Encrypted message:");
            string cript = CryptographyBusiness.EncryptedToString(CryptographyBusiness.Cryptograph(simetricKey,msg));
            Console.WriteLine(cript);

            byte[] simetricKeyB = BouncyCastleKey.simetricKey(Key2.privateKey, Key1.publicKey);
            Console.WriteLine("Simetric Key (Other Side)");
            Console.WriteLine(CommonBusiness.bytesToString(simetricKeyB));

            Console.WriteLine("Decrypted message:");
            string decript = CryptographyBusiness.Decryptograph(simetricKeyB, CryptographyBusiness.StringToEncrypted(cript));
            Console.WriteLine(decript);
        }
    }
}
