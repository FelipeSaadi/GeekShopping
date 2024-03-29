using Duende.IdentityServer.Services;
using GeekShopping.IdentifyServer.Configuration;
using GeekShopping.IdentifyServer.Initializer;
using GeekShopping.IdentifyServer.Model;
using GeekShopping.IdentityServer.Services;
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
	options.Events.RaiseFailureEvents = true;
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

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();

var initializer = app.Services.CreateScope().ServiceProvider.GetService<IDbInitializer>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

initializer.Initialize();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
