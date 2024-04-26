using GestioneNotifiche.Core.Sessione;
using MasterSoft.Core.EndPoint.SetMstServicePolling;
using MasterSoft.Core.Logger;
using MasterSoft.Core.Sessione;
using Microsoft.AspNetCore.Mvc;
using ServiceAnalyzer.core.Database;
using ServiceAnalyzer.core.Database.Model;
using ServiceAnalyzer.Core.Logger;
using ServiceAnalyzer.WebService.Config;
using System.Reflection;
using System.Text.Json;

namespace ServiceAnalyzer.WebService.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class AnalyzerController : Controller
    {
        private MstmonitoraggioContext _dbContext;
        private ConfigurationOption _config;
        private Assembly _assembly;
        private ISessioneModel _sessione;
        private Logger _logger;
        private int _idService = 0;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AnalyzerController(ConfigurationOption config)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            try
            {
                Initialize(config);

                if (_logger != null)
                {
                    _logger.Info("Inizializzazione Controller: AnalyzerController", _idService, ETipoLog.Info.ToString(), "AnalyzerControllerConstructor");
                    _logger.Info("Variabili sessione:" + "\n" + JsonSerializer.Serialize(_sessione), _idService, ETipoLog.Info.ToString(), "AnalyzerControllerConstructor");
                    _logger.Info("Parametri di configurazione:" + "\n" + JsonSerializer.Serialize(_config), _idService, ETipoLog.Info.ToString(), "AnalyzerControllerConstructor");
                }
            }
            catch(Exception ex)
            {
                if(_logger != null)
                    _logger.Exception(DateTime.Now + " --- " + "Errore: ", ex, _idService, ETipoLog.Exception.ToString(), "AnalyzerControllerConstructor");
            }
        }
        [HttpPut("SetMSTServicePolling")]
        public IActionResult SetMSTServicePolling([FromBody]SetMSTServicePollingRequest request)
        {
            try
            {
                if ((string.IsNullOrEmpty(request.GuidServizio)) || (request.Tipo == 0) || (request.Metodo == 0))
                {
                    _logger.Info("Bad request recived:" + "\n" + JsonSerializer.Serialize(request), _idService, ETipoLog.Info.ToString(), "SetMSTServicePolling");
                    return BadRequest(ModelState);
                }               
                if (!_dbContext.Mstservices.Any(x => x.GuidServizio == request.GuidServizio))
                {
                    _logger.Info("Request ServiceGuid not found: " + "\n" + JsonSerializer.Serialize(request), _idService, ETipoLog.Info.ToString(), "SetMSTServicePolling");
                    ModelState.AddModelError("", "Request ServiceGuid not found");
                    return StatusCode(200, ModelState);
                }
                if(!_dbContext.MstservicesMethods.Any(x => x.Tipo == request.Metodo))
                {
                    _logger.Info("Request Method not found: " + "\n" + JsonSerializer.Serialize(request), _idService, ETipoLog.Info.ToString(), "SetMSTServicePolling");
                    ModelState.AddModelError("", "Request Method not found");
                    return StatusCode(200, ModelState);
                }

                _idService = _dbContext.Mstservices.First(x => x.GuidServizio == request.GuidServizio).IdService;
                var metodoServizio = _dbContext.MstservicesMethods.FirstOrDefault(x => x.Tipo == request.Metodo && x.IdService == _idService);
                var idMetodo = metodoServizio == null ? 0 : metodoServizio.IdMetodo;

                if(idMetodo == 0)
                {
                    _logger.Info("Request Method not found for service: " + "\n" + JsonSerializer.Serialize(request), _idService, ETipoLog.Info.ToString(), "SetMSTServicePolling");
                    ModelState.AddModelError("", "Request Method not found");
                    return StatusCode(200, ModelState);
                }
                var mstServicePoll = Build(_idService, idMetodo, request.Tipo, request.Messaggio);

                _logger.Info("Try insert into MSTServicePolling: " + "\n" + JsonSerializer.Serialize(mstServicePoll), _idService, ETipoLog.Info.ToString(), "SetMSTServicePolling");

                _dbContext.MstservicesPollings.Add(mstServicePoll);
                var res = _dbContext.SaveChanges();

                if (res > 0)
                {
                    _logger.Info("Insert into MSTServicePolling Succeded for record: " + "\n" + JsonSerializer.Serialize(mstServicePoll), _idService, ETipoLog.Info.ToString(), "SetMSTServicePolling");
                    return Ok();
                }
                else
                {
                    _logger.Info("Error impossible insert record into table MSTServicesPolling: " + "\n" + JsonSerializer.Serialize(mstServicePoll), _idService, ETipoLog.Info.ToString(), "SetMSTServicePolling");
                    ModelState.AddModelError("", "Error impossible insert record into table MSTServicesPolling");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex) 
            {
                _logger.Exception(DateTime.Now + " --- " + "Errore: ", ex, _idService, ETipoLog.Exception.ToString(), "SetMSTServicePolling");
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }
        }
        private void Initialize(ConfigurationOption config)
        {
            _config = config;
            _dbContext = new MstmonitoraggioContext(_config.ConnectionString);
            _assembly = Assembly.GetExecutingAssembly();
            _sessione = new SessioneRepository(_assembly, _config).Get();
            _logger = new Logger(_sessione, _dbContext);
            //qui metterò l'inizializzazione di tutti i repository
        }
        private MstservicesPolling Build(int idService, int idMetodo, int tipo, string messaggio)
        {
            var polling = new MstservicesPolling()
            {
                DataChiamata = DateTime.Now,
                IdMetodo = idMetodo,
                IdService = idService,
                Testo = messaggio,
                Tipo = tipo
            };
            return polling;
        }

    }
}
