using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoboticsPos.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using RoboticsPos.Data.Base;
using Newtonsoft.Json;

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
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DebtPayment DebtPayment { get; set; }
       

        private SqliteConnection connectiion;

    private List<EntityEntry> ModifiedEntities = new();

        private void AddIndex<TEntity>(ModelBuilder modelBuilder, string[] indexedColumns) where TEntity : class
        {
            modelBuilder.Entity<TEntity>().HasIndex(indexedColumns);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Expression<Func<AuditEntity, bool>> filter = s => !s.IsDeleted;
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.IsAssignableTo(typeof(AuditEntity)) && entityType.BaseType == null)
                {

                    var parameter = Expression.Parameter(entityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filter.Parameters.First(), parameter, filter.Body);
                    var lambda = Expression.Lambda(body, parameter);
                    var indexedColumns = new[] { "IsDeleted" };

                    // Create a generic method for adding an index
                    var addIndexMethod = GetType().GetMethod(nameof(AddIndex), BindingFlags.NonPublic | BindingFlags.Instance);
                    var genericAddIndexMethod = addIndexMethod.MakeGenericMethod(entityType.ClrType);

                    // Invoke the generic method to add the index
                    genericAddIndexMethod.Invoke(this, new object[] { modelBuilder, indexedColumns });

                    entityType.SetQueryFilter(lambda);
                }
            }

            modelBuilder.Entity<Shop>()
                .HasMany(a => a.Carts)
                .WithOne(s => s.Shop)
                .HasForeignKey(s => s.ShopId);

            modelBuilder.Entity<Debt>()
                .HasOne(s => s.Payment)
                .WithOne(s => s.Debt)
                .HasForeignKey<Debt>(s => s.PaymentId);

            modelBuilder.Entity<Payment>()
                .HasOne(a => a.Debt)
                .WithOne(s => s.Payment)
                .HasForeignKey<Payment>(s => s.DebtId);

            modelBuilder.Entity<DebtPayment>()
                .HasOne(dp => dp.Payment)
                .WithOne(p => p.DebtPayment)
                .HasForeignKey<DebtPayment>(dp => dp.PaymentId);

            modelBuilder.Entity<Debt>()
                .HasMany(s => s.DebtPayments)
                .WithOne(s => s.Debt)
                .HasForeignKey(s=>s.DebtId);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ModifiedEntities.AddRange(ChangeTracker.Entries().Where(s =>
                    s.State == EntityState.Added || s.State == EntityState.Modified || s.State == EntityState.Deleted)
                .ToList());
            ApplyAuditValues();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void ApplyAuditValues()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is AuditEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));


            foreach (var entityEntry in entries)
            {
                ((AuditEntity)entityEntry.Entity).ModifiedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
                if (entityEntry.State == EntityState.Deleted)
                {
                    ((AuditEntity)entityEntry.Entity).IsDeleted = true;
                    entityEntry.State = EntityState.Modified;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                
                    if (((AuditEntity)entityEntry.Entity).CreatedDate == DateTime.MinValue)
                       ((AuditEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }


                var entityEntryStr = JsonConvert.SerializeObject(entityEntry.Entity, Newtonsoft.Json.Formatting.Indented,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                var state = entityEntry.State;

            }
        }
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
