using GeekShopping.IdentifyServer.Configuration;
using GeekShopping.IdentifyServer.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
	connection, new MySqlServerVersion(new Version(8, 0, 5)))
);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<MySQLContext>()
	.AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{
	options.Events.RaiseErrorEvents = true;
	options.Events.RaiseInformationEvents = true;
	options.Events.RaiseSuccessEvents = true;
	options.EmitStaticAudienceClaim = true;
})
	.AddInMemoryIdentityResources(
		IdentityConfiguration.IdentityResources
	)
	.AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
	.AddInMemoryClients(IdentityConfiguration.Clients)
	.AddAspNetIdentity<ApplicationUser>()
	.AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
