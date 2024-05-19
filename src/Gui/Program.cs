using Gui.controllers;
using General.config;
using General;
using Microsoft.AspNetCore.Authentication.Cookies;

Logger.Logger.SetLogfile("log.txt");
Logger.Logger.Info("Begins session");
IConfigCreator cfgCreator = new PostgresConfigCreator();
IConfig cfg = cfgCreator.Create("config.json");
ControllerManager mng = new(cfg);

AuthController.SetInnerManager(mng);
ProfileController.SetInnerManager(mng);
FirmController.SetInnerManager(mng);
ProductController.SetInnerManager(mng);
AdminController.SetInnerManager(mng);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = new PathString("/Login");
        option.AccessDeniedPath = new PathString("/AccessDenied");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
