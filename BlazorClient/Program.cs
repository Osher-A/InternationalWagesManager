using BlazorClient;
using BlazorClient.ApiServices;
using BlazorClient.AuthenticationService;
using BlazorClient.Utilities;
using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmployeeRepository, EmployeeApiRepo>();
builder.Services.AddScoped<ICurrenciesRepository, CurrencyApiRepo>();
builder.Services.AddScoped<IWConditionsRepository, WorkConditionsApiRepo>();
builder.Services.AddScoped<ISalaryComponentsRepository, SalaryComponentsApiRepo>();
builder.Services.AddScoped<ISalaryRepository, SalaryApiRepo>();
builder.Services.AddScoped<BlazorMessages>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

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

