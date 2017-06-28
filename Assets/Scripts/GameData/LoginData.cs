using System;
namespace Stars
{
    public class LoginData
    {
        static private LoginData loginData_;
        public string userName;
        public string password;
        public uint _selectServer;
        public UInt64 _user_id = 0;
        public uint _role_id = 0;
        public Byte[] session = new Byte[32];

        public string _appKey = "";
        public bool _checkLogin = false;    //判断是否已经登录到了渠道的服务器

        //选择不同的平台
        public enum CHOOSEDIFFPLATFORM
        {
            LOCAL
        }

        public CHOOSEDIFFPLATFORM _platform = CHOOSEDIFFPLATFORM.LOCAL;    //选择不同的平台

        public LoginData()
        {

        }

        static public LoginData getInstance()
        {
            if (loginData_ == null)
            {
                loginData_ = new LoginData();
            }
            return loginData_;
        }

    }
}