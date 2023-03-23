using Azure.Core;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Bearer and then token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
builder.Services.AddScoped<IWConditionsRepository, WConditionsRepository>();
builder.Services.AddScoped<ISalaryComponentsRepository, SalaryComponentsRepository>();
builder.Services.AddScoped<ISalaryRepository, SalaryRepository>();
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MyDbContext>();

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.

// To add all migrations if they do not exist like when moving to another machine
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<MyDbContext>())
{
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Global exception handler
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}


app.UseHttpsRedirection();
app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().AllowAnonymous();

app.Run();

//app.MapPost("/login", async (LoginDto loginDto, UserManager<IdentityUser> _userManager) =>
//{
//    var user = await _userManager.FindByNameAsync(loginDto.Username);

//    if (user is null)
//    {
//        return Results.Unauthorized();
//    }
//    var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

//    if (!isValidPassword)
//    {
//        return Results.Unauthorized();
//    }

//    // Generate an access token
//    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]));
//    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//    var roles = await _userManager.GetRolesAsync(user);
//    var claims = await _userManager.GetClaimsAsync(user);
//    var tokenClaims = new List<Claim>
//    {
//        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
//        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//        new Claim(ClaimTypes.Email, user.Email),
//        new Claim("email_confirmed", user.EmailConfirmed.ToString())
//    }.Union(claims)
//    .Union(roles.Select(role => new Claim(ClaimTypes.Role, role)));

//    var securityToken = new JwtSecurityToken(
//        issuer: builder.Configuration["JwtSettings:Issuer"],
//        audience: builder.Configuration["JwtSettings:Audience"],
//        claims: tokenClaims,
//        expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(builder.Configuration["JwtSettings:DurationInMintues"])),
//        signingCredentials: credentials
//    );

//    var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);


//    var response = new AuthResponseDto
//    {
//        UserId = user.Id,
//        Username = user.UserName,
//        Token = accessToken
//    };

//    return Results.Ok(response);
//}).AllowAnonymous();




//internal class LoginDto
//{
//    public string Username { get; set; }
//    public string Password { get; set; }
//}

//internal class AuthResponseDto
//{
//    public string UserId { get; set; }
//    public string Username { get; set; }
//    public string Token { get; set; }
//}
