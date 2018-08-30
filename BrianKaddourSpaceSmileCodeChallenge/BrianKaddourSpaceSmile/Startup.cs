using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpaceSmileBrianKaddour.ApplicationCore.Clients;
using SpaceSmileBrianKaddour.ApplicationCore.Interfaces;
using SpaceSmileBrianKaddour.Web.Extensions;
using SpaceSmileBrianKaddour.Web.Interfaces;

namespace BrianKaddourSpaceSmile
{
    public class Startup
    {
        private IServiceCollection _services;
        private readonly ILogger<Startup> _logger;

        public IConfiguration Configuration { get; }

        public Startup(ILogger<Startup> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            //Use singleton to inject for accessing config vars
            services.AddSingleton<IConfiguration>(Configuration);
            _logger.LogInformation("ConfigureServices called");


            //Inject the API Client
            services.AddHttpClient<ILaunchpadApiClient, LaunchPadInfoClient>();

            //
            services.AddHttpClient<ILaunchpadService, LaunchPadService> ();
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogInformation("Configure called");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
