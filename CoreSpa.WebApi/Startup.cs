using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace CoreSpa.WebApi
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

            services.AddMvc()
                    .AddJsonOptions(config =>
                    {
                        config.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                        config.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });

            // services.AddSingleton<IFileProvider>(
            //          new PhysicalFileProvider(
            //              Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


            //For Cors Setting
            services.AddCors(options =>
            {
                options.AddPolicy("Trust", p =>
                {
                    //todo: Get from confiuration
                    p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                });
            });

            // Todo : Comment out this line (Imp)
            // // In production, the Angular files will be served from this directory
            // services.AddSpaStaticFiles(configuration =>
            // {
            //     configuration.RootPath = "ClientApp/dist";
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("Trust");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            // if (env.IsDevelopment())
            // {
            //     //app.UseSpaStaticFiles();
            //     app.UseSpa(spa =>
            //     {
            //         spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
            //         spa.Options.SourcePath = "ClientApp";
            //             // this is calling the start found in package.json
            //             spa.UseAngularCliServer(npmScript: "start");
            //     });
            // }


            // for each angular client we want to host. 
            app.Map(new PathString("/clientapp1"), client =>
            {
                if (env.IsDevelopment())
                {
                    StaticFileOptions clientApp1Dist = new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(
                                Path.Combine(
                                    Directory.GetCurrentDirectory(),
                                    @"ClientApp1"
                                )
                            )
                    };
                    client.UseSpaStaticFiles(clientApp1Dist);
                    client.UseSpa(spa =>
                    {
                        spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                        spa.Options.SourcePath = "ClientApp1";
                        
                        // it will use package.json & will search for start command to run
                        spa.UseAngularCliServer(npmScript: "start");
                    });

                }
                else
                {
                    // Each map gets its own physical path
                    // for it to map the static files to. 
                    StaticFileOptions clientApp1Dist = new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(
                                Path.Combine(
                                    Directory.GetCurrentDirectory(),
                                    @"ClientApp1/dist"
                                )
                            )
                    };

                    // Each map its own static files otherwise
                    // it will only ever serve index.html no matter the filename 
                    client.UseSpaStaticFiles(clientApp1Dist);

                    // Each map will call its own UseSpa where
                    // we give its own sourcepath
                    client.UseSpa(spa =>
                    {
                        spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                        spa.Options.SourcePath = "ClientApp1";
                        spa.Options.DefaultPageStaticFileOptions = clientApp1Dist;
                    });

                }

            });

            // for each angular client we want to host. 
            app.Map(new PathString("/clientapp2"), client =>
            {
                if (env.IsDevelopment())
                {
                    StaticFileOptions clientApp1Dist = new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(
                                Path.Combine(
                                    Directory.GetCurrentDirectory(),
                                    @"ClientApp2"
                                )
                            )
                    };
                    client.UseSpaStaticFiles(clientApp1Dist);
                    client.UseSpa(spa =>
                    {
                        spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                        spa.Options.SourcePath = "ClientApp2";

                        // it will use package.json & will search for start command to run
                        spa.UseAngularCliServer(npmScript: "start");
                    });

                }
                else
                {
                    // Each map gets its own physical path
                    // for it to map the static files to. 
                    StaticFileOptions clientApp1Dist = new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(
                                Path.Combine(
                                    Directory.GetCurrentDirectory(),
                                    @"ClientApp2/dist"
                                )
                            )
                    };

                    // Each map its own static files otherwise
                    // it will only ever serve index.html no matter the filename 
                    client.UseSpaStaticFiles(clientApp1Dist);

                    // Each map will call its own UseSpa where
                    // we give its own sourcepath
                    client.UseSpa(spa =>
                    {
                        spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                        spa.Options.SourcePath = "ClientApp2";
                        spa.Options.DefaultPageStaticFileOptions = clientApp1Dist;
                    });

                }

            });



        }
    }
}
