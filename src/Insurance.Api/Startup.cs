using Insurance.Api.Configuration;
using Insurance.Bll;
using Insurance.Bll.Services.Interfaces;
using Insurance.Dal;
using Insurance.Dal.Context;
using Insurance.Dal.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Insurance.Api
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
            services.AddSwaggerGen();
            services.AddDbContext<InsuranceDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("dbConnection"), item => item.MigrationsAssembly("Insurance.Api")));
            services.AddSingleton<IRequestService>(x => new RequestService(Configuration.Get<AppConfiguration>().ProductApi));
            services.AddScoped<ISurchargeRepository, SurchargeRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<ISurchargeService, SurchargeService>();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
