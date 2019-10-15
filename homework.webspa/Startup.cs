using homework.webspa.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SolrNet;
using SolrNet.Impl;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
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

            services.AddHttpClient("solrapi", c => { c.BaseAddress = new Uri(solrEndpoint); })
                    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpClientFactory httpClient)
        {
            InitSolr(env, httpClient).GetAwaiter().GetResult();

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

        private async Task InitSolr(IHostingEnvironment env, IHttpClientFactory httpClient)
        {
            var solr = new SolrInitializer(httpClient, env);

            await solr.Initialize();
        }
    }
}
