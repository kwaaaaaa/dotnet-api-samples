using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APICollection
{
    public class Startup
    {
        public static string _weatherSecret = null;
        public static string _cryptoSecret = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _weatherSecret = Configuration["WeatherKey"];
            _cryptoSecret = Configuration["CryptoKey"];
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "weather",
                    template: "{controller=Weather}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "crypto",
                    template: "{controller=Crypto}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "currency",
                    template: "{controller=Currency}/{action=Index}/{id?}");
            });
        }
    }
}
