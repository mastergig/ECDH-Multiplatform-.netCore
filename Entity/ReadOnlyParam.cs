using System.Security.Cryptography;

namespace ECDiffieHellman_Bouncy_Castle.Entity
{
    public static class ReadOnlyParam
    {
        public static readonly int ivSize = 24;
        public static readonly int keySize = 256;
        public static readonly string sha = "SHA256";
        
        public static class PemWritter
        {
            public static string beginPublic = "-----BEGIN PUBLIC KEY-----\r\n";
            public static string beginPrivate = "-----BEGIN EC PRIVATE KEY-----\r\n";
            public static string endPublic = "\r\n-----END PUBLIC KEY-----\r\n";
            public static string endPrivate = "\r\n-----END EC PRIVATE KEY-----\r\n";
            public static string regex = "(-----BEGIN PUBLIC KEY-----|-----END PUBLIC KEY-----|-----BEGIN EC PRIVATE KEY-----|-----END EC PRIVATE KEY-----|\r\n|\r|\n)";
        }
    }
}