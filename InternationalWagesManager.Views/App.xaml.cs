using InternationalWagesManager.DAL;
using InternationalWagesManager.Domain;
using InternationalWagesManager.Domain.Utilities;
using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows;

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


            // To add all migrations if they do not exist like when moving to another machine
            using (var scope = app.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<MyDbContext>())
            {
                context.Database.Migrate();
            }

            // TESTER
            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var salaryRepo = scope.ServiceProvider.GetRequiredService<ISalaryRepository>();
            //    var wCRepo = scope.ServiceProvider.GetRequiredService<IWConditionsRepository>();
            //    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            //    var domain = new SalaryManager(mapper, salaryRepo, wCRepo);
            //    domain.AddSalaryAsync();
            //}

        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>();
            // options = >   options.UseSqlServer("Server =.\\SQLEXPRESS; Database = IWagesManager; Trusted_Connection = True; MultipleActiveResultSets = true; "));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(Assembly.Load(typeof(DTOConvertor).Assembly.FullName));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IWConditionsRepository, WConditionsRepository>();
            services.AddScoped<ISalaryComponentsRepository, SalaryComponentsRepository>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();
            services.AddScoped<ISalaryRepository, SalaryRepository>();
            services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
            services.AddScoped<EmployeeManager>();
            services.AddScoped<WorkConditionsManager>();
            services.AddScoped<SalaryComponentsManager>();
            services.AddScoped<CurrenciesManager>();
            services.AddScoped<SalaryManager>();
            services.AddScoped<PaymentsManager>();
            services.AddScoped<StatementManager>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }


    }
}
