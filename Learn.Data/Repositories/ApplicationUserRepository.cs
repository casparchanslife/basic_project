using Learn.Data.Infrastructure;
using Learn.Data.Infrastructure.Interface;
using Learn.Data.Repositories.Interface;
using Learn.DataModel.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Learn.Data.Repositories
{
    public class ApplicationUserRepository: RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return  await DataContext.Users.FirstOrDefaultAsync(o=> o.Id.ToString() == id);
        }
    }

}