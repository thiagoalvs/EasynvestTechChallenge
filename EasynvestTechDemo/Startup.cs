using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EasynvestTechDemo.Application.DTOs;
using EasynvestTechDemo.Application.Factories;
using EasynvestTechDemo.Application.Interfaces;
using EasynvestTechDemo.Application.Interfaces.Infrastrucutre;
using EasynvestTechDemo.Application.Middlewares;
using EasynvestTechDemo.Application.Services;
using EasynvestTechDemo.Application.ViewModels;
using EasynvestTechDemo.Domain.Enums;
using EasynvestTechDemo.Domain.Models;
using EasynvestTechDemo.Infrastructure.Services.Fundos;
using EasynvestTechDemo.Infrastructure.Services.RendaFixa;
using EasynvestTechDemo.Infrastructure.Services.TesouroDireto;
using EasynvestTechDemo.Shared.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace EasynvestTechDemo
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
            //Add Swagger
            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();

            //Add IOptions 
            services.Configure<ExternalServicesConfiguration>(options => Configuration.GetSection("ExternalServices").Bind(options));

            //Add AutoMapper
            services.AddSingleton(new AutoMapper.MapperConfiguration(config =>
            {
                config.CreateMap<CustomerInvestments, InvestmentsDetailedViewModel>()
                .ForMember(vm => vm.AmountTotal, inv => inv.MapFrom(x => x.AmountTotal))
                .ForMember(vm => vm.Investments, inv => inv.MapFrom(x => x.Investments.Select(item => new InvestmentsDetailedViewModelItem() { Amount = item.Amount, DrawAmount = item.DrawAmount, ExpireDate = item.ExpireDate, IncomeTax = item.IncomeTax, InvestedAmount = item.InvestedAmount, Name = item.Name })));

                //External Services
                config.CreateMap<FundoServiceResponseItem, InvestmentDTO>();
                config.CreateMap<RendaFixaServiceResponseItem, InvestmentDTO>();
                config.CreateMap<TesouroDiretoServiceResponseItem, InvestmentDTO>();
            }).CreateMapper());

            //Add HttpClientFactory with retry policy
            var httpDefaultPolicy = Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(retryCount: 2, sleepDurationProvider: (retryCount) => TimeSpan.FromSeconds(3));

            services.AddHttpClient("Default", (client) =>
            {
                //client.Timeout = TimeSpan.FromSeconds(5);
            }).AddPolicyHandler(httpDefaultPolicy);

            //Add LocalCache
            services.AddMemoryCache();

            //Add DI
            services.AddScoped<IInvestmentsService, InvestmentsService>();
            services.AddScoped<IExternalInvestmentService, FundosService>();
            services.AddScoped<IExternalInvestmentService, RendaFixaService>();
            services.AddScoped<IExternalInvestmentService, TesouroDiretoService>();

            services.AddSingleton<IInvestmentFactory, InvestmentFactory>();

            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Configure Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //Cofigure ErrorHandlingMiddleware Middleware
            app.UseMiddleware<ErrorHandlingMiddleware>();

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
