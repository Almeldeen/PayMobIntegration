using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PayMobIntegration.Repository;
using PayMobIntegration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Paymob.CashIn;

namespace PayMobIntegration
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
            services.AddHttpClient();
            services.AddControllersWithViews();
            services.AddScoped<IPayment, Paymob>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddPaymobCashIn(config =>
            {
                config.ApiKey = "ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SnVZVzFsSWpvaWFXNXBkR2xoYkNJc0ltTnNZWE56SWpvaVRXVnlZMmhoYm5RaUxDSndjbTltYVd4bFgzQnJJam8zTURnNU5UWjkuY0EzNTdqakowZm9MYU9jUTZzUjFpYmtRZlkxOWdsbnpCeFh6ZHp4MUwyS252Y3JsVUNVVV9NTjc1NUZCSWpJOGk0YW1ERVRiVXplRF9XcG9ITk82bUE=";
                config.Hmac = "DB68DBEBF6AABCF809FC2FA2C746BE7D";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
