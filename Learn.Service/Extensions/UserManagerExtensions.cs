using Learn.DataModel.Models;
using Learn.Service.Manager;
using System.Linq;

namespace Learn.Service.Extensions
{
    public static class UserManagerExtensions
    {
        public static ApplicationUser FindByPhoneNumberAsync(this ApplicationUserManager um, string phoneNumber)
        {
            return um?.Users?.SingleOrDefault(x => x.PhoneNumber == phoneNumber);
        }

    }
}