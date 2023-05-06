using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Core;
using Quartz.Impl;
using System.Collections.Specialized;
using Task_Scheduler_App.Application.Repository.Interface;
using Task_Scheduler_App.Application.Services;
using Task_Scheduler_App.Infrastructure.MailHelper;
using Task_Scheduler_App.Infrastructure.QuartzService;
using Task_Scheduler_App.Infrastructure.QuartzService.Jobs;
using Task_Scheduler_App.Infrastructure.Repository;
using Task_Scheduler_App.Infrastructure.Services;

namespace Task_Scheduler_Application
{
    public class Startup
    {
        private IConfiguration _config;
        private string hostingEnv;

        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env, IConfiguration config)
        {
            _config = config;
            hostingEnv = env.EnvironmentName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Quarz Configuration 
            string? connectionString = _config.GetSection("QuartzConnection").Value;
            services.AddQuartz(opt =>
            {
                opt.UsePersistentStore(s =>
                {
                    s.UseSqlServer(connectionString);
                    s.UseJsonSerializer();
                });
            });
            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
            });

            RegisterDependecies(services);
        }

        //register services
        private void RegisterDependecies(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDapper, Dapperr>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITaskSchedulerServices, TaskSchedulerServices>();
            services.AddTransient<IPeopleBirthDayService, PeopleBirthDayService>();
            services.AddTransient<InitializeJob>();
            services.AddTransient<EmaiHelper>();
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
