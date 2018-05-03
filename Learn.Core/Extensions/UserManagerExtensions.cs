﻿using Learn.Core.DataModels;
using Learn.Core.Manager;
using System.Linq;

namespace Learn.Core.Extensions
{
    public static class UserManagerExtensions
    {
        public static ApplicationUser FindByPhoneNumberAsync(this ApplicationUserManager um, string phoneNumber)
        {
            return um?.Users?.SingleOrDefault(x => x.PhoneNumber == phoneNumber);
        }

    }
}