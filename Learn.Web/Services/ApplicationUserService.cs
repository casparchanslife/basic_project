using Learn.Lib.Infrastructure.Interface;
using Learn.Data.Repositories.Interface;
using System.Threading.Tasks;
using Learn.DataModel;

namespace Learn.Web.Services
{

    public interface IApplicationUserService
    {
        Task<ApplicationUser> GetUser(string userId);
       void SaveUser();
    }

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IUnitOfWork unitOfWork;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository, IUnitOfWork unitOfWork)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IUserService Members

        public async Task<ApplicationUser> GetUser(string userId)
        {
            return await applicationUserRepository.GetUserById(userId);
        }
        
       public void SaveUser()
        {
            unitOfWork.Commit();
        }
      
        #endregion
    }
}