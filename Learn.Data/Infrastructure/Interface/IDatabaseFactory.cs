using System;

namespace Learn.Data.Infrastructure.Interface
{
    public interface IDatabaseFactory : IDisposable
    {
        ApplicationDbContext Get();
    }
}
