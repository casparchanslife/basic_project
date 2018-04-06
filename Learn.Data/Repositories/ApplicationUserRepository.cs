using Learn.Data.Infrastructure;
using Learn.Data.Infrastructure.Interface;
using Learn.Data.Repositories.Interface;
using Learn.DataModel.Models;
namespace Learn.Data.Repositories
{
    public class ApplicationUserRepository: RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            }        
        }

}