using System;

namespace ECDiffieHellman_Bouncy_Castle.Business
{
    public static class CommonBusiness
    {
        public static string bytesToString(byte[] entrada)
        {
            string ret = Convert.ToBase64String(entrada);
            return ret;
        }
        public static byte[] stringToBytes(string entrada)
        {
            byte[] ret = Convert.FromBase64String(entrada);
            return ret;
        }
    }
}