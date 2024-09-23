using System;
using BookInventoryApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using Microsoft.EntityFrameworkCore;


namespace BookInventory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Razor Pages support
            services.AddRazorPages();

            // Add Blazor Server support
            services.AddServerSideBlazor();

            // Uncomment and configure the WeatherForecastService if needed
            // services.AddSingleton<WeatherForecastService>();

            // Add Controllers with Views support (optional, if using MVC)
            services.AddControllersWithViews();

            // Add an HTTP client service with the base address configured
            services.AddHttpClient("LocalApi", client => 
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
            });

            // Add a singleton service for managing state
            services.AddSingleton<StateContainer>();

            // Register Syncfusion Blazor service
            services.AddSyncfusionBlazor();

             // Register the book service interface and implementation
            services.AddScoped<iBookServices, BookService>();

            // Register Syncfusion license key (replace with your actual license key)
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF5cWWtCf1NpR2FGfV5ycEVHYlZQTHxeRE0DNHVRdkdnWXZccnVXR2leWURxXks=");

            // Add DbContext and other services here if needed...

             // Register the DbContext with the SQL Server provider
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Use detailed error pages in development
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Use generic error handling and HSTS in production
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Enable serving static files
            app.UseStaticFiles();

            // Enable routing
            app.UseRouting();

            // Configure the HTTP request pipeline
            app.UseEndpoints(endpoints =>
            {
                // Map Blazor SignalR hub
                endpoints.MapBlazorHub();

                // Map the default Razor page (Fallback for unmatched routes)
                endpoints.MapFallbackToPage("/_Host");

                // Map default controller route (optional, if using MVC)
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
