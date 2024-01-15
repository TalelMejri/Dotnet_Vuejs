using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class DbContextClasse : DbContext
    {

        public DbSet<Todo> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=LAPTOP-JDBLJIM4\SQLEXPRESS; initial catalog=TodosDb; integrated security=SSPI;TrustServerCertificate=True");
        }
    }
}
