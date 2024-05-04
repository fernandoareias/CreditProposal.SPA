using System;
using Atividade02.Proposals.Domain.Data.Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using System.Text;
using Atividade02.Proposals.Infrastructure;
using Atividade02.Proposals.Application;
using Atividade02.Core.MessageBus.Configurations;
using Atividade02.Proposals.API.Configurations.Serilog;
using Atividade02.Proposals.API.BackgroundServices;
using Atividade02.Proposals.API.Protos.Services;

namespace Atividade02.Proposals.API.Configurations
{
    public static class ApiConfigurations
    {
        public static void ApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddGrpc();

            services.AddHostedService<ProposalSentEventWorker>();
            services.AddHostedService<ProposalCreatedEventWorker>();
            services.AddHostedService<PreAnalysisPolicySuccessfullyExecutedEventWorker>();
            services.AddHostedService<FraudAnalysisPolicySuccessfullyExecutedEventWorker>();

            ApiInjection(services, configuration);
        }

        public static void UseApiConfiguration(this WebApplication app, IConfiguration configuration)
        {
            app.UseLogs(configuration);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProposalServices>();

                // Outros endpoints REST...

                endpoints.MapControllers();
            });
        }

        private static void ApiInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddProposalInfrastructure(configuration);
            services.AddProposalApplication();
        }
    }
}

