using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Learn.DataModel.Models;

namespace Learn.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("Name=MainDbContext") { }

        public DbSet<Note> Note { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual void Commit()
        {
            base.SaveChanges();
        }

    }
}
