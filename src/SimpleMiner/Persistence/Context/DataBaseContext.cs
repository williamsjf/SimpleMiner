using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SimpleMiner.Persistence.Context
{
    public class DataBaseContext : DbContext
    {
        public string ConnectionString { get; set; }
        public DataBaseContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public int SaveChanges(int bacthSize)
        {
            if (this.ChangeTracker.Entries().Count() >= bacthSize)
            {
                return base.SaveChanges();
            }

            return 0;
        }
    }
}
