using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
   public class DbContextClasse : DbContext
    {

        public DbSet<UploadedFile> fileInfo { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=LAPTOP-JDBLJIM4\SQLEXPRESS; initial catalog=NeoBd; integrated security=SSPI;Trust Server Certificate=true");
        }
    }
}
