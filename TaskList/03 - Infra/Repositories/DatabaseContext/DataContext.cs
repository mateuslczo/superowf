using Microsoft.EntityFrameworkCore;
using TaskList._01___Domain;

namespace TaskList._03___Infra.Repositories.DatabaseContext
{

    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Tasks> Tasks { get; set; }


    }
}