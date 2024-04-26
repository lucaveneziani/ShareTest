namespace MasterSoft.Core.Mail
{
    public interface IEmailSender
    {
        string SendEmail(IEmailNotifica message);
    }
}
