using Learn.Data.Infrastructure;
using Learn.Data.Infrastructure.Interface;
using Learn.Data.Infrastructure.Repositories.Interface;
using Learn.DataModel.Models;

namespace Learn.Lib.Infrastructure.Repositories
{
    public class NoteRepository : RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        
    }
}
