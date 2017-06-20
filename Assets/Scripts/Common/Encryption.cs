namespace Stars
{
    public class Encryption
    {
        static public byte[] key = { 0x31, 0xb2, 0xda, 0xfa, 0x4f, 0x9e, 0x22, 0xac, 0xd5, 0xce };
        static public bool _isUseEncryption = false;
        static public byte[] NetMsgEncryp(byte[] byteMessage)
        {
            if (_isUseEncryption )
            {
                for (int i = 0; i < byteMessage.Length; i++)
                {
                    byteMessage[i] = (byte)(byteMessage[i] ^ key[i % key.Length]);
                }
            }
            return byteMessage;
        }

        static public byte[] NetMsgDecryp(byte[] byteMessage)
        {
            byte[] byteMessage_clone = byteMessage.Clone() as byte[];
            return NetMsgEncryp(byteMessage_clone);
        }

        static public byte[] NetMsgDecrypWithoutCopy(byte[] byteMessage)
        {
            return NetMsgEncryp(byteMessage);
        }

    }
}