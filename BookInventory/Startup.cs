using System;
using BookInventoryApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;

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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddControllersWithViews();
            services.AddHttpClient("LocalApi", client => client.BaseAddress = new Uri("https://localhost:5001/"));
            services.AddSingleton<StateContainer>();

            // Register Syncfusion Blazor service
            //services.AddSyncfusionBlazor();  // Ensure this is recognized

            // Register Syncfusion license key
           
        //   Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("
            
        //     ");

            // Add DbContext and other services...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline...
        }
    }
}
