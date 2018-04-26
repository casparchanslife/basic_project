using Learn.Data.Infrastructure.Interface;

namespace Learn.Data.Infrastructure
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
