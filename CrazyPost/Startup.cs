using CrazyPost.Context;
using CrazyPost.Contexts;
using CrazyPost.Models;
using CrazyPost.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CrazyPost
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
            //it can be used if we want to use localDb as DB. Connection string already has been added
            services.AddDbContext<Contexts.ApiDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CrazyPostContext")));
            //services.AddDbContext<ApiDbContext>(opt => opt.UseInMemoryDatabase());

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc();



            //using Dependency Injection
            services.AddScoped<IPostRepository, PostRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApiDbContext dbContext, ILoggerFactory logFactory)
        {
            logFactory.AddConsole(Configuration.GetSection("Logging"));
            logFactory.AddDebug();

            DbInitializer.Initialize(dbContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }


    }
}
