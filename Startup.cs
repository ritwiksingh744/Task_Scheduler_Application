using Microsoft.Extensions.Configuration;

namespace Task_Scheduler_Application
{
    public class Startup
    {
        private IConfiguration _config;
        private string hostingEnv;


        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            hostingEnv = env.EnvironmentName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            RegisterDependecies(services);
        }

        private void RegisterDependecies(IServiceCollection services)
        {
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (hostingEnv == "local")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("Anonymous");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}
