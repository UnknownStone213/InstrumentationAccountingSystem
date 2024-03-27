using Microsoft.EntityFrameworkCore;
using InstrumentationAccountingSystem;
using InstrumentationAccountingSystem.Models;
using InstrumentationAccountingSystem.Mapper;
using InstrumentationAccountingSystem.BusinessLogic.Interfaces;
using InstrumentationAccountingSystem.BusinessLogic.Services;
using AutoMapper;

//using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();
//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITypeService, TypeService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<IInstrumentationService, InstrumentationService>();
builder.Services.AddTransient<IVerificationService, VerificationService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
});
var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
