﻿using CustomIdentityStore.Models;
using CustomIdentityStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CustomIdentityStore
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddDefaultTokenProviders();

			// Sets up the Identity auth cookie for the application
			// Keeps the user in a cookie for the ExpireTimeSpan, to prevent having to log in again
			// Since SlidingExpiration is true, it will renew the cookie
			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.Name = ".CustomIdentityStore";
				options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
				options.Cookie.Expiration = TimeSpan.FromDays(30);

				options.ExpireTimeSpan = TimeSpan.FromDays(30);
				options.SlidingExpiration = true;
			});

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			// The Distributed Memory Cache is required for the Session to be maintained
			// It can also function and a general cache
			services.AddDistributedMemoryCache();

			// Sets up the session idle timeout cookie
			// This will cause the user to get logged out if they have the page open and idle for more than the IdleTimeout
			services.AddSession(options =>
			{
				options.Cookie.Name = ".CustomIdentityStore.Session";
				options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
				options.Cookie.Expiration = TimeSpan.FromDays(1);

				options.IdleTimeout = TimeSpan.FromMinutes(30);
			});

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseSession();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}