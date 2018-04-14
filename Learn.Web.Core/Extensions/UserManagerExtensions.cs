using Learn.DataModel.Models;
using Learn.Web.Core.Manager;
using System.Linq;

namespace Learn.Web.Core.Extensions
{
    public static class UserManagerExtensions
    {
        public static ApplicationUser FindByPhoneNumberAsync(this ApplicationUserManager um, string phoneNumber)
        {
            return um?.Users?.SingleOrDefault(x => x.PhoneNumber == phoneNumber);
        }

    }
}