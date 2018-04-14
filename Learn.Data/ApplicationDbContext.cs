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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(a => a.UpdatedBy)
                .WithMany()
                .HasForeignKey(a => a.UpdatedById);
            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(a => a.CreatedBy)
                .WithMany()
                .HasForeignKey(a => a.CreatedById);
        }
    }
}
