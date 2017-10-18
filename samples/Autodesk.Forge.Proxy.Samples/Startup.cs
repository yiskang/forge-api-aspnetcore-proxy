//
// Copyright (c) Autodesk, Inc. All rights reserved
// Copyright (c) .NET Foundation. All rights reserved.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// 
// Forge Proxy
// by Eason Kang - Autodesk Developer Network (ADN)
//

#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autodesk.Forge.Proxy;
#endregion

namespace Autodesk.Forge.Proxy.Samples
{
    public class Startup
    {
        private static string PROXY_URI = @"/forgeproxy";

        public Startup(IHostingEnvironment env)
        {
            Environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IHostingEnvironment Environment { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddForgeProxy(options => {
                options.ProxyUri = PROXY_URI;

                var clientId = Configuration.GetSection("Credentials:ClientId").Value;
                var clientSecret = Configuration.GetSection("Credentials:ClientSecret").Value;

                if( string.IsNullOrWhiteSpace(clientId) || clientId == "<YOUR_CLIENT_ID>" )
                {
                    throw new ArgumentNullException("The value of Credentials:ClientId in appsettings.json is invalid");
                }

                if( string.IsNullOrWhiteSpace(clientSecret) || clientId == "<YOUR_CLIENT_SECRET>" )
                {
                    throw new ArgumentNullException("The value of Credentials:ClientSecret in appsettings.json is invalid");
                }

                options.ClientId = clientId;
                options.ClientSecret = clientSecret;

                options.PrepareRequest = ( originalRequest, message ) =>
                {
                    message.Headers.Add("X-Forwarded-Host", originalRequest.Host.Host);
                    return Task.FromResult(0);
                };
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForgeProxy();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Viewer}/{action=Index}/{id?}"
                );
            });
        }
    }
}
