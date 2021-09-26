using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedKernel.Extensions;
using MediatR;
using LetsCode.Api.Domain.Services;
using LetsCode.Api.Domain.AggregateRebelde;
using LetsCode.Api.Infrastructure;
using LetsCode.Api.Application.Queries;

namespace LetsCode.Api
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
            services.ConfigureSwagger();

            services.AddMediatR(typeof(Startup));
            services.AddScoped<IRebeldeDomainService, RebeldeDomainService>();
            services.AddScoped<IRebeldeRepository, RebeldeRepository>();
            services.AddScoped<IRebeldeQueries, RebeldeQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUICustom();

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
