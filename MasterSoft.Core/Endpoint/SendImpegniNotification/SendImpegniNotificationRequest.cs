namespace MasterSoft.Core.EndPoint.SendImpegniNotification
{
    public class SendImpegniNotificationRequest
    {
        public string ClientEmail { get; set; } = "";
        public string NotificationText { get; set; } = "";
    }
}
