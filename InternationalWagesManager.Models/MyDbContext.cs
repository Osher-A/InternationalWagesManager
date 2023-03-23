using InternationalWagesManager.Models.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.Models
{
    public class MyDbContext : IdentityDbContext
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

            //modelBuilder.Entity<Currency>().HasData(
            //    new Currency
            //    {
            //        Id = 1,
            //        Name = "CHF",
            //        Description = "Swiss Franc"
            //    },
            //    new Currency
            //    {
            //        Id = 2,
            //        Name = "EUR",
            //        Description = "Euro"
            //    },
            //    new Currency
            //    {
            //        Id = 3,
            //        Name = "GBP",
            //        Description = "Sterling Pound"
            //    },
            //    new Currency
            //    {
            //        Id = 4,
            //        Name = "ILS",
            //        Description = "Israeli Shekel"
            //    },
            //    new Currency
            //    {
            //        Id = 5,
            //        Name = "USD",
            //        Description = "United States Dollar"
            //    }
            //    );

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "d1b5952a-2162-46c7-b29e-1a2a68922c14",
                    Name = "Employer",
                    NormalizedName = "EMPLOYER"
                },
                new IdentityRole
                {
                    Id = "42358d3e-3c22-45e1-be81-6caa7ba865ef",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                }
            );

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(
                    new IdentityUser
                    {
                        Id = "408aa945-3d84-4421-8342-7269ec64d949",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "ADMIN@LOCALHOST.COM",
                        NormalizedUserName = "ADMIN@LOCALHOST.COM",
                        UserName = "admin@localhost.com",
                        PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                        EmailConfirmed = true
                    },
                    new IdentityUser
                    {
                        Id = "3f4631bd-f907-4409-b416-ba356312e659",
                        Email = "user@localhost.com",
                        NormalizedEmail = "USER@LOCALHOST.COM",
                        NormalizedUserName = "USER@LOCALHOST.COM",
                        UserName = "user@localhost.com",
                        PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                        EmailConfirmed = true
                    }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                     new IdentityUserRole<string>
                     {
                         RoleId = "d1b5952a-2162-46c7-b29e-1a2a68922c14",
                         UserId = "408aa945-3d84-4421-8342-7269ec64d949",
                     },
                    new IdentityUserRole<string>
                    {
                        RoleId = "42358d3e-3c22-45e1-be81-6caa7ba865ef",
                        UserId = "3f4631bd-f907-4409-b416-ba356312e659",
                    }
                );


            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }


    }

}
