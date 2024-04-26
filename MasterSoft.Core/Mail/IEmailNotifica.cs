using MimeKit;

namespace MasterSoft.Core.Mail
{
    public interface IEmailNotifica
    {
        List<MailboxAddress> To { get; set; }
        string Subject { get; set; }
        string Content { get; set; }
    }
}
