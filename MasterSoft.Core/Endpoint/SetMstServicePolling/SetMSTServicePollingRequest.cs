namespace MasterSoft.Core.EndPoint.SetMstServicePolling
{
    public class SetMSTServicePollingRequest
    {
        public string GuidServizio { get; set; } = "";
        public int Tipo { get; set; }
        public int Metodo { get; set; }
        public string Messaggio { get; set; } = "";
    }
}
