using Learn.DataModel;
using Learn.Web.Manager;
using System.Linq;

namespace Learn.Web.Extensions
{
    public static class UserManagerExtensions
    {
        public static ApplicationUser FindByPhoneNumberAsync(this ApplicationUserManager um, string phoneNumber)
        {
            return um?.Users?.SingleOrDefault(x => x.PhoneNumber == phoneNumber);
        }

    }
}