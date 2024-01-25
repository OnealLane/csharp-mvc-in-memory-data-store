namespace exercise.wwwapi.Data
{
    using exercise.wwwapi.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;


    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        public DbSet<Product> products { get; set; }
    }
}
