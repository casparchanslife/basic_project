using Learn.Core.Infrastructure;
using Learn.Core.Infrastructure.Interface;
using Learn.Core.DataModels;

namespace Learn.Core.Repositories
{

    #region Interface
    public interface INoteRepository : IRepository<Note>
    {
    }
    #endregion

    public class NoteRepository : RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}
