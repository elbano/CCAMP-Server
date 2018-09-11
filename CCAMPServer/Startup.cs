using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCAMPServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CCAMPServer
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var rootConnectionStr = String.Format(Configuration.GetConnectionString("RootConnection"), Environment.MachineName);

            // Add EF support for SqlServer
            services.AddEntityFrameworkSqlServer();
            // Add ApplicationDBContext
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(rootConnectionStr));

            DBContextFactory.AddConnectionString(DBContextFactory.ROOT, rootConnectionStr);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ApplicationLogging.ConfigureLogger(Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();
                // Create the DB if it doesn't exist and applies any pending migration
                dbContext.Database.Migrate();

                if (!dbContext.Database.EnsureCreated())
                {
                    //Seed the DB
                    DBSeeder.Seed(dbContext, Configuration);
                }
            }
        }
    }
}
