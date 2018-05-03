using Learn.Core.Infrastructure.Interface;

namespace Learn.Core.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private ApplicationDbContext dataContext;

        public DatabaseFactory(ApplicationDbContext dbContext)
        {
            dataContext = dbContext;
        }

        public ApplicationDbContext Get()
        {
            return dataContext;
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
            }
        }
    }
}
