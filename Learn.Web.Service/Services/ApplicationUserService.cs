using System.Collections.Generic;
using System.Linq;
using Learn.DataModel.Models;
using Learn.Core;
using Learn.Lib.Infrastructure.Interface;
using Learn.Data.Repositories.Interface;

namespace Learn.Service
{

    public interface IApplicationUserService
    {
       ApplicationUser GetUser(string userId);
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

        public ApplicationUser GetUser(string userId)
        {
            //return applicationUserRepository.Get(u => u== userId);
            return null;
        }
        
       public void SaveUser()
        {
            unitOfWork.Commit();
        }
      
        #endregion
    }
}
