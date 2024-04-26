using GestioneNotifiche.Core.Sessione;
using MasterSoft.Core.EndPoint.SendImpegniNotification;
using MasterSoft.Core.Logger;
using MasterSoft.Core.Sessione;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceAnalyzer.core.Database;
using ServiceAnalyzer.Core.Logger;
using ServiceAnalyzer.WebService.Code;
using ServiceAnalyzer.WebService.Config;
using System.Reflection;
using System.Text.Json;

namespace ServiceAnalyzer.WebService.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private MstmonitoraggioContext _dbContext;
        private ConfigurationOption _config;
        private Assembly _assembly;
        private ISessioneModel _sessione;
        private Logger _logger;
        private int _idService = 0;
        private IHubContext<NotificationHub> _notificationHub;
        private ConnectionMapping _connectionMapping;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public NotificationController(ConfigurationOption config, IHubContext<NotificationHub> notificationHub, ConnectionMapping connectionMapping)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            try
            {
                Initialize(config, notificationHub, connectionMapping);

                if (_logger != null)
                {
                    _logger.Info("Inizializzazione Controller: NotificationController", _idService, ETipoLog.Info.ToString(), "NotificationControllerConstructor");
                    _logger.Info("Variabili sessione:" + "\n" + JsonSerializer.Serialize(_sessione), _idService, ETipoLog.Info.ToString(), "NotificationControllerConstructor");
                    _logger.Info("Parametri di configurazione:" + "\n" + JsonSerializer.Serialize(_config), _idService, ETipoLog.Info.ToString(), "NotificationControllerConstructor");
                }
            }
            catch(Exception ex)
            {
                if(_logger != null)
                    _logger.Exception(DateTime.Now + " --- " + "Errore: ", ex, _idService, ETipoLog.Exception.ToString(), "NotificationControllerConstructor");
            }
        }
        [HttpPut("SendImpegniNotification")]
        public async Task<IActionResult> SendImpegniNotification([FromBody] SendImpegniNotificationRequest request)
        {
            try
            {
                var connectionId = _connectionMapping.GetConnectionId(request.ClientEmail);

                if (connectionId != null)
                {
                    _logger.Info("Try to send at ConnectionId: " + connectionId + " the reminder: " + "\n" + JsonSerializer.Serialize(request), _idService, ETipoLog.Info.ToString(), "SetMSTServicePolling");
                    await _notificationHub.Clients.Client(connectionId).SendAsync("Notification", request.NotificationText);
                    return StatusCode(200);
                }
                else
                {
                    _logger.Info("Unable to found ConnectionId for reminder: " + "\n" + JsonSerializer.Serialize(request), _idService, ETipoLog.Info.ToString(), "SetMSTServicePolling");
                    return StatusCode(204);
                }
            }
            catch (Exception ex) 
            {
                _logger.Exception(DateTime.Now + " --- " + "Errore: ", ex, _idService, ETipoLog.Exception.ToString(), "SendImpegniNotification");
                return StatusCode(500); 
            }
        }
        private void Initialize(ConfigurationOption config, IHubContext<NotificationHub> notificationHub, ConnectionMapping connectionMapping)
        {
            _config = config;
            _dbContext = new MstmonitoraggioContext(_config.ConnectionString);
            _assembly = Assembly.GetExecutingAssembly();
            _sessione = new SessioneRepository(_assembly, _config).Get();
            _logger = new Logger(_sessione, _dbContext);
            _notificationHub = notificationHub;
            _connectionMapping = connectionMapping;
            //qui metterò l'inizializzazione di tutti i repository
        }
    }
}
