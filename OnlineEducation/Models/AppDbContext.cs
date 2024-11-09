using Microsoft.EntityFrameworkCore;

namespace OnlineEducation.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<adminlogin> adminlogin { get; set; }
        public DbSet<slider> slider { get; set; }
        public DbSet<manageuser> manageuser { get; set; }
        public DbSet<managecategory> managecategory { get; set; }
        public DbSet<register> register { get; set; }
        public DbSet<Update> Update { get; set; }
        public DbSet<reader> reader { get; set; }
        public DbSet<managecourse> managecourse { get; set; }
        public DbSet<managecontact> managecontact { get; set; }
        public DbSet<managechangepassword> managechangepassword { get; set; }
        public DbSet<purchase> purchase { get; set; }
    }
}