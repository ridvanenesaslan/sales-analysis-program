using LogBilisim.Web.Data.Context;
using LogBilisim.Web.Data.DataSeed;
using LogBilisim.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlserverConnection")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddNotyf(conf =>
{
    conf.DurationInSeconds = 2;
    conf.IsDismissable = true;
    conf.Position = NotyfPosition.TopRight;
});

var app = builder.Build();
app.UseNotyf();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSeedUser();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
