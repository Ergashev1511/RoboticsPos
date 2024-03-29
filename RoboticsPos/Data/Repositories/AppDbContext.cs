using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoboticsPos.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Data.Repositories
{
    public  class AppDbContext  : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }


        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CashBox> CashBoxes { get; set; }
        public DbSet<CheckPrintingData> CheckPrintingData { get; set; }
        public DbSet<Discount> Discounts { get; set; }


        private SqliteConnection connectiion;




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            connectiion ??= InitiializeSqliteConnection();

            optionsBuilder.UseSqlite(connectiion);

            base.OnConfiguring(optionsBuilder);
        }

        private static SqliteConnection InitiializeSqliteConnection()
        {
            var connectionsttring = new SqliteConnectionStringBuilder()
            {
                DataSource = Variablies.StaticVariablies.DataBaseName,
            };
            return new SqliteConnection(connectionsttring.ToString());
            
        }
    }
}
