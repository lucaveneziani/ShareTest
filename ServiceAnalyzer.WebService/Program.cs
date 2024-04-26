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
builder.Services.AddCors();

var app = builder.Build();

//set hub address for exchange with signalIR
var hubHost = "/hubs/notification";
//app.MapHub<NotificationHub>(hubHost);

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();


//set address of the client for signalIR outside of the current net
app.UseCors(options => options.WithOrigins(cfgOptions.BdmonitorAddress).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>(hubHost);
app.Run();


