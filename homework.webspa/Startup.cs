using homework.webspa.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolrNet;
using SolrNet.Impl;
using System;
using web.Model;

namespace homework.webspa
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            //services.AddNodeServices();

            // should add dev prod support
            string solrEndpoint = Configuration["SolrEndpoint"];

            services.AddSolrNet<Building>(solrEndpoint + "buildings");
            services.AddSolrNet<Lock>(solrEndpoint + "locks");
            services.AddSolrNet<Group>(solrEndpoint + "groups");
            services.AddSolrNet<Medium>(solrEndpoint + "media");

            services.AddTransient(typeof(ISearchService), typeof(SolrService));
        }

        public void InitializeSolr(ISearchService searchService)
        {
            //   var result =  nodeServices.InvokeAsync<int>("ClientApp/public/SolrSetup/InitSolrDocuments.js").GetAwaiter().GetResult();
            //   searchService.AddCores();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //  InitializeSolr(searchService);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
