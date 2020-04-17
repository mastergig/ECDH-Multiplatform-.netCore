namespace ECDiffieHellman_Bouncy_Castle.Entity
{
    public class Keys
    {
        public string privateKey { get; set; }
        public string publicKey { get; set; }
    }

    public class EncryptedMessage
    {
        public string iv { get; set; }
        public string encryptedText { get; set; }
    }
}