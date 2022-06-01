using BootstrapBlazor.Components;
using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.DTO;
using InternationalWagesManager.Domain;
using System.Reflection;
using InternationalWagesManager.Domain.Utilities;

namespace InternationalWagesManager.Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // TESTER
            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var repo = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
            //    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            //    var employee = new DTO.Employee() { Email = "omoscovitch@gmail.com", FirstName = "aharon", LastName = "moscovitch", DOB = new DateTime(1984, 06, 04), Phone = "07928593096" };
            //    var domain = new EmployeeManager(mapper, repo);
            //    domain.UpdateEmployee(employee);
            //}

        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>();
            // options = >   options.UseSqlServer("Server =.\\SQLEXPRESS; Database = IWagesManager; Trusted_Connection = True; MultipleActiveResultSets = true; "));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(Assembly.Load(typeof(DTOConvertor).Assembly.FullName));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }


    }
}
