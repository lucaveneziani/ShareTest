using Microsoft.Extensions.Options;
using ServiceAnalyzer.WebService.Code;
using ServiceAnalyzer.WebService.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen(); 
builder.Services.AddSignalR(options => { options.EnableDetailedErrors = true; });
builder.Services.AddSingleton<ConnectionMapping>();

var cfgOptions = new ConfigurationOption();
cfgOptions = builder.Configuration
                        .GetSection(nameof(ConfigurationOption))
                        .Get<ConfigurationOption>();
builder.Services.AddSingleton(cfgOptions);
builder.Services.AddCors();

var app = builder.Build();

//set hub address for exchange with signalIR
var hubHost = "/hubs/notification";
//app.MapHub<NotificationHub>(hubHost);

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();


//set address of the client for signalIR outside of the current net
//le origini vanno modificate per ogni ambiente in cui viene installato
app.UseCors(options => 
    options.WithOrigins("https://bdmonitor.fastera.net", "https://www.bdmonitor.it", "https://bdtest.fastera.net")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>(hubHost);
app.Run();


