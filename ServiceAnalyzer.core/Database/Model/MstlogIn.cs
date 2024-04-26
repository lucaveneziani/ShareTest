namespace ServiceAnalyzer.Core.Database.Model;

public partial class MstlogIn
{
    public int IdLogIn { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
