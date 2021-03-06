﻿using Learn.Data.Infrastructure.Interface;
using Learn.DataModel.Models;
using System.Threading.Tasks;

namespace Learn.Data.Repositories.Interface
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
       Task<ApplicationUser>  GetUserById(string id);
    }

}
