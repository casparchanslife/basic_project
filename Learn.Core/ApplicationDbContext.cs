using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Learn.Core.DataModels;

namespace Learn.Core
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("Name=MainDbContext") { }

        public DbSet<fmk_function> fmk_function { get; set; }
        public DbSet<fmk_function_group> fmk_function_group { get; set; }
        public DbSet<fmk_function_role> fmk_function_role { get; set; }

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
