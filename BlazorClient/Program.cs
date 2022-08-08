using AutoMapper;
using BlazorClient;
using BlazorClient.ApiServices;
using InternationalWagesManager.DAL;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IEmployeeRepository, EmployeeApiRepo>();
//ApiTester();
await builder.Build().RunAsync();

 async void ApiTester()
{
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var repo = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
        var employees = await repo.GetEmployeesAsync();
        var a = employees.Count;

    }


}

