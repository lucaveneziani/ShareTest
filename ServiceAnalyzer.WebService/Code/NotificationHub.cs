using GestioneNotifiche.Core.Sessione;
using MasterSoft.Core.Logger;
using MasterSoft.Core.Sessione;
using Microsoft.AspNetCore.SignalR;
using ServiceAnalyzer.core.Database;
using ServiceAnalyzer.Core.Logger;
using ServiceAnalyzer.WebService.Config;
using System.Reflection;
using System.Text.Json;

namespace ServiceAnalyzer.WebService.Code
{
    public class NotificationHub : Hub
    {
        private ConnectionMapping _connectionMapping;
        private MstmonitoraggioContext _dbContext;
        private ConfigurationOption _config;
        private Assembly _assembly;
        private ISessioneModel _sessione;
        private Logger _logger;
        private int _idService = 0;

        public NotificationHub(ConfigurationOption config, ConnectionMapping connectionMapping)
        {
            try
            {
                Initialize(config, connectionMapping);
            }
            catch (Exception ex) 
            {
                //if (_logger != null)
                    _logger.Exception(DateTime.Now + " --- " + "Errore: ", ex, _idService, ETipoLog.Exception.ToString(), "OnConnectedAsync");
            }
        }
        public override async Task OnConnectedAsync()
        {
            // Store the email address and connection ID mapping when a client connects
            try
            {
                //TODO da cancellare i log quando funziona
                _logger.Info("Context:" + JsonSerializer.Serialize(Context), _idService, ETipoLog.Info.ToString(), "OnConnectedAsync");
                var httpContext = Context.GetHttpContext();
                _logger.Info("HTTPContext:" + JsonSerializer.Serialize(Context), _idService, ETipoLog.Info.ToString(), "OnConnectedAsync");
                var email = httpContext.Request.Query["username"].ToString();
                _logger.Info("email:" + JsonSerializer.Serialize(Context), _idService, ETipoLog.Info.ToString(), "OnConnectedAsync");
                var connectionId = Context.ConnectionId;
                _logger.Info("connectionId:" + JsonSerializer.Serialize(Context), _idService, ETipoLog.Info.ToString(), "OnConnectedAsync");

                if (email != null)
                    _connectionMapping.Add(email, connectionId);
            }
            catch (Exception ex)
            {
                _logger.Exception(DateTime.Now + " --- " + "Errore: ", ex, _idService, ETipoLog.Exception.ToString(), "OnConnectedAsync");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Remove the mapping when a client disconnects
            try
            {
                var httpContext = Context.GetHttpContext();
                var email = httpContext.Request.Query["username"].ToString();
                if (email != null)
                    _connectionMapping.Remove(email);
            }
            catch(Exception ex) 
            {
                _logger.Exception(DateTime.Now + " --- " + "Errore: ", ex, _idService, ETipoLog.Exception.ToString(), "OnDisconnectedAsync");
            }
            await base.OnDisconnectedAsync(exception);
        }
        private void Initialize(ConfigurationOption config, ConnectionMapping connectionMapping)
        {
            _config = config;
            _dbContext = new MstmonitoraggioContext(_config.ConnectionString);
            _assembly = Assembly.GetExecutingAssembly();
            _sessione = new SessioneRepository(_assembly, _config).Get();
            _logger = new Logger(_sessione, _dbContext);
            _connectionMapping = connectionMapping;
            //qui metterò l'inizializzazione di tutti i repository
        }
    }
}
