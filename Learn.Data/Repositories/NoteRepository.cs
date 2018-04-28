using Learn.Data.Infrastructure;
using Learn.Data.Infrastructure.Interface;
using Learn.DataModel.Models;

namespace Learn.Lib.Infrastructure.Repositories
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
