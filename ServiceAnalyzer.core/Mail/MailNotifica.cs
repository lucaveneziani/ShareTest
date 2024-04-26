using MasterSoft.Core.Mail;
using MimeKit;
using ServiceAnalyzer.core.Database.Model;
using ServiceAnalyzer.Core.Database.Model;

namespace ServiceAnalyzer.Core.Mail
{
    public class MailNotifica : IEmailNotifica
    {
        public List<MailboxAddress> To { get; set; } = new List<MailboxAddress>();
        public string Subject { get; set; } = "";
        public string Content { get; set; } = "";
        public MailNotifica(IEnumerable<string> to, string subject, string content)
        {
            Subject = subject;
            Content = content;
        }
        public MailNotifica(IEnumerable<string> to, MstservicesPolling exception, ETipoMonitoringPolling tipoMonitoring)
        {
            To.AddRange(to.Select(x => new MailboxAddress(x.Trim(), x.Trim())));

            if (tipoMonitoring == ETipoMonitoringPolling.Exception)
            {
                Subject = "Eccezione trovata in data: " + exception.DataChiamata + " per il servizio con ID: " + exception.IdService;
                Content = exception.Testo;
            }
            if(tipoMonitoring == ETipoMonitoringPolling.Info)
            {
                Subject = "Ultimo pollings valido ricevuto in data: " + exception.DataChiamata + " per il servizio con ID: " + exception.IdService;
                Content = "Attenzione! verificare le funzionalità del servizio con ID: " + exception.IdService + " poichè non risultano più chiamate per il metodo con ID: " + exception.IdMetodo;
            }
        }
    }
}
