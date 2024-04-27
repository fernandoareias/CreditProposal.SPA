using Atividade02.Proposals.API.Configurations;
using Atividade02.Proposals.API.Configurations.Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLogs(builder.Configuration, "microservices-proposal");
 
builder.Services.ApiConfiguration(builder.Configuration);

var app = builder.Build();

app.UseApiConfiguration(builder.Configuration);
app.Run();

