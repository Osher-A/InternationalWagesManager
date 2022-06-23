using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using InternationalWagesManager.Models.Configurations;

namespace InternationalWagesManager.Models
{
    public class MyDbContext :DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkConditions> WorkConditions { get; set; }
        public DbSet<SalaryComponents> SalariesComponents { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database = IWagesManager; Trusted_Connection = True; MultipleActiveResultSets = true;")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(ed => ed.Email).IsUnique();

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
