using InternationalWagesManager.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkConditions> WorkConditions { get; set; }
        public DbSet<SalaryComponents> SalariesComponents { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database = IWagesManager; Trusted_Connection = True;Integrated Security=True; MultipleActiveResultSets = true;Encrypt=True; TrustServerCertificate=True;")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(ed => ed.Email).IsUnique();

            modelBuilder.Entity<WorkConditions>().Property(wc => wc.Deductions).HasPrecision(9, 8);
            modelBuilder.Entity<Salary>().Property(s => s.ExpensesRate).HasPrecision(9, 5);
            modelBuilder.Entity<Salary>().Property(s => s.WageRate).HasPrecision(9, 5);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
