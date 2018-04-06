using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Learn.WebAPIService.Manager
{
    public class UserClaimManager
    {
        public const string facebook = "facebook";
        public const string app = "app";
        private static int GlobalRequestIndex = 0;
        private static CultureInfo provider = CultureInfo.InvariantCulture;

        public static UserClaimModel GetUserClaim(IIdentity identity)
        {
            GlobalRequestIndex = (GlobalRequestIndex >= 1000) ? 0 : GlobalRequestIndex + 1;
            UserClaimModel model = new UserClaimModel(GlobalRequestIndex);
            if (identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)identity;
                var claims = claimsIdentity.Claims.ToList();
                model.deviceId = claims.FirstOrDefault(o => o.Type == "deviceId").Value;
                model.status = claims.FirstOrDefault(o => o.Type == "status").Value;
                model.since = DateTime.ParseExact(claims.FirstOrDefault(o => o.Type == "since").Value, "yyyy-MM-dd HH:mm:ss", provider);

                model.accountID = String.IsNullOrEmpty(claims.FirstOrDefault(o => o.Type == "accountID").Value) ? Guid.Empty : new Guid(claims.FirstOrDefault(o => o.Type == "accountID").Value);
                model.accountName = claims.FirstOrDefault(o => o.Type == "accountName").Value;
               
            }
            return model;
        }
    }

    [DataContract]
    public class UserClaimModel
    {
        public UserClaimModel(int GlobalRequestIndex)
        {
            tid = GlobalRequestIndex;
            accountID = Guid.Empty;
        }

        [DataMember]
        public int tid { get; set; }

        [DataMember]
        public string deviceId { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public DateTime since { get; set; }

        [DataMember]
        public Guid accountID { get; set; }
        [DataMember]
        public string accountName { get; set; }


    }
}
