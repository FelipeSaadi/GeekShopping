﻿using GeekShopping.IdentifyServer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
{
	public class MySQLContext : IdentityDbContext<ApplicationUser>
	{
		public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

	}
}
