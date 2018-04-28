using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Lib
{
    public class OAuthGlobal
    {
        public static string OAuth_Client_Id_iOS = ConfigurationManager.AppSettings["OAuth.Client.Id.iOS"];
        public static string OAuth_Client_Id_Android = ConfigurationManager.AppSettings["OAuth.Client.Id.Android"];
        public static string OAuth_Client_Secret = ConfigurationManager.AppSettings["OAuth.Client.Secret"];

        public static int OAuth_Token_Expire_Minute = String.IsNullOrEmpty(ConfigurationManager.AppSettings["OAuth.Token.Expire.Minute"]) ? 30 : int.Parse(ConfigurationManager.AppSettings["OAuth.Token.Expire.Minute"]);
        public static int OAuth_RefreshToken_Expire_Minute = String.IsNullOrEmpty(ConfigurationManager.AppSettings["OAuth.RefreshToken.Expire.Minute"]) ? 60 : int.Parse(ConfigurationManager.AppSettings["OAuth.RefreshToken.Expire.Minute"]);

        public static int OAuth_Token_Limit = (ConfigurationManager.AppSettings["OAuth.Token.Limit"] != null ? Int32.Parse(ConfigurationManager.AppSettings["OAuth.Token.Limit"]) : 20);
    }
}
