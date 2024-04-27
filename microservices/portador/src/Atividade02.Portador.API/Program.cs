using Atividade02.Core.MessageBus.Configurations;
using Atividade02.Core.MessageBus.Services;
using Atividade02.Core.MessageBus.Services.Interfaces;
using Atividade02.Proponent.API.BackgroundServices;
using Atividade02.Proponent.API.Configurations.Serilog;
using Atividade02.Proponent.API.Data;
using Atividade02.Proponent.API.Data.Common.Interfaces;
using Atividade02.Proponent.API.Data.Repositories;
using Atividade02.Proponent.API.Models.Interfaces;
using Atividade02.Proponent.API.Models.Interfaces.Services;
using Atividade02.Proponent.API.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddLogs(builder.Configuration, "microservices-portador");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MessageBusConfigs>(
                   builder.Configuration.GetSection(nameof(MessageBusConfigs)));

builder.Services.AddScoped<IMongoContext, MongoContext>();
builder.Services.AddScoped<IUnitOfWork, MongoContext>(); 
builder.Services.AddSingleton<IMessageBus, MessageBus>();

builder.Services.AddScoped<IPortadorRepository, PortadorRepository>();
builder.Services.AddScoped<IPortadorServices, PortadorServices>();

builder.Services.AddHostedService<ProposalApprovedEventWorker>();

var app = builder.Build();

app.UseLogs(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

