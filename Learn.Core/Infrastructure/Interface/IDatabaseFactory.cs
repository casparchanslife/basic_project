using System;

namespace Learn.Core.Infrastructure.Interface
{
    public interface IDatabaseFactory : IDisposable
    {
        ApplicationDbContext Get();
    }
}
