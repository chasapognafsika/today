using Client.Services;
using Clients.Data;
using Clients.Domain.Providers;
using Clients.Domain.Services;
using Clients.Provider.EntityFramework;
using InMemoryDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlDb;

namespace Hexagonal3
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
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IQueryClientsService, QueryClientsService>();

            services.AddScoped<IClientProvider, ClientProvider>();
            services.AddScoped<IQueryClientsProvider, QueryClientsProvider>();

            services.AddScoped<IDbContext, SqlDbContext>();

            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlDb")));

            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
