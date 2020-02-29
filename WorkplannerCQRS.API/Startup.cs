using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkplannerCQRS.API.Data;

namespace WorkplannerCQRS.API
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
            services.AddControllers();
            
            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<WorkplannerDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("WorkplannerDb"));
                });
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // only run migrations in development mode
                using var scope = app.ApplicationServices.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<WorkplannerDbContext>();
                db.Database.Migrate();
                
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}