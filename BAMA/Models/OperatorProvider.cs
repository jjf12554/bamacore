using BAMA.Tools;
using WEBTools;

namespace BAMA.Models
{
    public class OperatorProvider
    {
        public static OperatorProvider Provider
        {
            get { return new OperatorProvider(); }
        }
        private string LoginUserKey = "jxbama20190413";
        private string LoginProvider = "Cookie";

        public OperatorModel GetCurrent()
        {
            OperatorModel operatorModel = new OperatorModel();
            if (LoginProvider == "Cookie")
            {
                operatorModel = new OperatorModel() {
                    UserId = "9f2ec079-7d0f-4fe2-90ab-8b09a8302aba",
                    IsSystem = true
                };
                //operatorModel = EncryptTool.DESDecrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<OperatorModel>();
            }
            else
            {
                //operatorModel = EncryptTool.DESDecrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<OperatorModel>();
            }
            return operatorModel;
        }
        public void AddCurrent(OperatorModel operatorModel)
        {
            if (LoginProvider == "Cookie")
            {
                //WebHelper.WriteCookie(LoginUserKey, EncryptTool.DESEncrypt(operatorModel.ToJson()), 60 * 6);
            }
            else
            {
                //WebHelper.WriteSession(LoginUserKey, EncryptTool.DESEncrypt(operatorModel.ToJson()));
            }
            //WebHelper.WriteCookie("nfine_mac", EncryptTool.md5(Net.GetMacByNetworkInterface().ToJson(), 32));
            //WebHelper.WriteCookie("nfine_licence", Licence.GetLicence());
        }
        public void RemoveCurrent()
        {
            if (LoginProvider == "Cookie")
            {
                //WebHelper.RemoveCookie(LoginUserKey.Trim());
            }
            else
            {
                //WebHelper.RemoveSession(LoginUserKey.Trim());
            }
        }
    }
}
