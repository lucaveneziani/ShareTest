namespace MasterSoft.Core.Mail
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; set; }
        int Port { get; set; }
        string From { get; set; }
        string MailUserName { get; set; }
        string MailPassword { get; set; }
    }
}
