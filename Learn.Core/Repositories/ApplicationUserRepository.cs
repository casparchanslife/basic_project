﻿using Learn.Core.Infrastructure;
using Learn.Core.Infrastructure.Interface;
using Learn.Core.DataModels;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Learn.Core.Repositories
{
    #region Interface
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserById(string id);
    }
    #endregion

    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await DataContext.Users.FirstOrDefaultAsync(o => o.Id.ToString() == id);
        }
    }

}