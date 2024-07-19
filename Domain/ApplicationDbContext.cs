
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace TeleperformanceTask.Model
{
    public class ApplicationDBContext: DbContext
    {
       public virtual DbSet<Employee> employees { get; set; }
       public virtual DbSet<User> users { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
      : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Employe;Integrated Security=True;Encrypt=False;");
        }
        
    }
}
