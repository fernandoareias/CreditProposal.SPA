using Atividade02.BFF.Data;
using Atividade02.BFF.Data.Common.Interfaces;
using Atividade02.Core.Common.Validators;
using Atividade02.Core.Common.Validators.Interfaces;
using Atividade02.Core.MessageBus.Configurations;
using Atividade02.Core.MessageBus.Services;
using Atividade02.Core.MessageBus.Services.Interfaces;
using Atividade04.BFF.Configurations;
using Atividade04.BFF.Data.Repositories;
using Atividade04.BFF.Hubs;
using Atividade04.BFF.Models.Interfaces;
using Atividade04.BFF.Models.Interfaces.Repositories;
using Atividade04.BFF.Models.Interfaces.Services;
using Atividade04.BFF.Services;
using Elastic.CommonSchema;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(e => {
    e.MaximumReceiveMessageSize = 102400000;
});
builder.Services.AddGrpc();

builder.Services.Configure<AuthenticationConfiguration>(
                    builder.Configuration.GetSection(nameof(AuthenticationConfiguration)));


builder.Services.Configure<MessageBusConfigs>(
                   builder.Configuration.GetSection(nameof(MessageBusConfigs)));

builder.Services.AddScoped<IRetailerServices, RetailerServices>();
builder.Services.AddScoped<IRetailerRepository, RetailerRepository>();
builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IValidatorServices, ValidatorServices>();
builder.Services.AddScoped<IMongoContext, MongoContext>();
builder.Services.AddScoped<IUnitOfWork, MongoContext>();
builder.Services.AddSingleton<IMessageBus, MessageBus>();


builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:3000")
    .AllowCredentials();
}));

var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

//app.UseAuthorization();
//app.UseAuthentication();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ProposalHub>("/proposals");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});


app.Run();

