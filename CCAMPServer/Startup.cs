using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCAMPServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
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

        private String environmentPolicy;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var rootConnectionStr = String.Format(Configuration.GetConnectionString("RootConnection"), Environment.MachineName);
            var transactionalConnectionStr = String.Format(Configuration.GetConnectionString("TransactionalConnection"), Environment.MachineName);

            // Add EF support for SqlServer
            services.AddEntityFrameworkSqlServer();
            // Add ApplicationDBContext
            services.AddDbContextPool<ApplicationDBContext>(options => options.UseSqlServer(rootConnectionStr), 6);
            services.AddDbContextPool<TransactionDBContext>(options => options.UseSqlServer(transactionalConnectionStr), 64);

            environmentPolicy = Configuration.GetValue<String>("EnvironmentPolicy");

            services.AddCors(options =>
            {
                // Create a policy for localhost and buildserver3 production, development and test deployment
                options.AddPolicy(environmentPolicy, builder => builder.WithOrigins(
                   Configuration.GetSection($"CORSPolicy").Get<String[]>()).AllowAnyMethod().AllowAnyHeader());
                // Enable CORS globally
                services.Configure<MvcOptions>(o =>
                {
                    o.Filters.Add(new CorsAuthorizationFilterFactory(environmentPolicy));
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ApplicationLogging.ConfigureLogger(Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Setup Cors for development and build
                app.UseCors(environmentPolicy);
            }
            else if (env.IsProduction() || env.IsStaging())
            {
                app.UseCors(environmentPolicy);
            }
            else
            {                
                Program.Shutdown();
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
