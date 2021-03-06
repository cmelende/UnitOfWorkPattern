using DataAccess.Interfaces;
using FluentNHibernate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;
using ORMEntityFramework;
using ORMNhibernate;
using AppContext = ORMEntityFramework.AppContext;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using SessionSource = ORMNhibernate.SessionSource;

namespace UnitOfWork
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
            //Entity Framework
            services.AddDbContext<AppContext>();
            services.AddScoped<DbContext, AppContext>();
            services.AddScoped<IUnitOfWork<DbContext>, EfUnitOfWork>();
            services.AddTransient(typeof(IEfRepository<>), typeof(EfRepository<>));
            
            //Nhibernate
            services.AddScoped<IUnitOfWork<ISession>, NhUnitOfWork>();
            services.AddSingleton<ISessionSource, SessionSource>();
            services.AddTransient(typeof(INhRepository<>), typeof(NhRepository<>));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}