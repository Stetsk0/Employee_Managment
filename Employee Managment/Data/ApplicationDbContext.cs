using Employee_Managment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Employee_Managment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Credentials> Credentials { get; set; } = null!;
        public DbSet<Statistics> Statistics { get; set; } = null!;
        public DbSet<Vacation> Vacations { get; set; } = null!;
        public DbSet<Penalty> Penalties { get; set; } = null!;

        public ApplicationDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=employees.db");
        }

    }
}
