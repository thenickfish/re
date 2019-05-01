using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace api
{
    public class Startup
    {
        private readonly ILoggerFactory loggerFactory;

        public Startup (IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            this.loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);

            services.AddDbContext<RoomMateDbContext> ();

            services.AddSwaggerGen (c =>
            {
                c.SwaggerDoc ("v1", new Info { Title = "Roomease API", Version = "v1" });
            });

            services.AddMassTransit (ConfigureBus (), loggerFactory);

            services.AddSingleton<IHostedService, BusService> ();

        }

        private IBusControl ConfigureBus () =>
            Bus.Factory.CreateUsingRabbitMq (cfg =>
            {
                var host = cfg.Host (new Uri ("rabbitmq://localhost"), h =>
                {
                    h.Username ("guest");
                    h.Password ("guest");
                });

                cfg.ReceiveEndpoint (host, "submit-order", ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry (x => x.Interval (2, 100));

                    ep.Consumer (() => new TestConsumer ());
                });
            });

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseSwagger ();
            app.UseSwaggerUI (c =>
            {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc ();
        }
    }
}

public class BusService:
    IHostedService
    {
        private readonly IBusControl _busControl;

        public BusService (IBusControl busControl)
        {
            _busControl = busControl;
        }

        public Task StartAsync (CancellationToken cancellationToken)
        {
            return _busControl.StartAsync (cancellationToken);
        }

        public Task StopAsync (CancellationToken cancellationToken)
        {
            return _busControl.StopAsync (cancellationToken);
        }
    }