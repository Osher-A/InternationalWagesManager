using AutoMapper;
using BlazorClient;
using BlazorClient.ApiServices;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using InternationalWagesManager.DAL;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmployeeRepository, EmployeeApiRepo>();

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();


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

