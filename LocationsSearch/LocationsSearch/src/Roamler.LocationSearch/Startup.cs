using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;
using Roamler.LocationSearch.Contracts.Responses;
using Roamler.LocationSearch.Infrastructure;

namespace Roamler.LocationSearch
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
            services.AddTransient<IElasticService, ElasticService>();

            services.AddSingleton<IElasticClient>(GetElasticClient());

        }

        private IElasticClient GetElasticClient()
        {
            var elasticHost = Configuration.GetValue<string>("ElasticHost");
            var elasticUserName = Configuration.GetValue<string>("ElasticUserName");
            var elasticPassword = Configuration.GetValue<string>("ElasticPassword");
            var elasticDefaultIndex = Configuration.GetValue<string>("ElasticDefaultIndex");

            var node = new Uri(elasticHost);
            var settings = new ConnectionSettings(node)
                                .BasicAuthentication(elasticUserName, elasticPassword)
                                .DefaultIndex(elasticDefaultIndex)
                                .DefaultMappingFor<ElasticResponse>(m => m
                                .PropertyName(p => p.Address, "Address")
                                .PropertyName(p => p.Geoip, "geoip")
            );

            return new ElasticClient(settings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
