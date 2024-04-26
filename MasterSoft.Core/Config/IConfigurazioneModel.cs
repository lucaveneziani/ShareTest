namespace MasterSoft.Core.Config
{
    public interface IConfigurazioneModel
    {
        public string ConnectionString { get; set; } 
        public int LogDaysBackup { get; set; }
    }
}
