using Microsoft.EntityFrameworkCore;
using ServiceAnalyzer.core.Database.Model;
using ServiceAnalyzer.Core.Database.Model;

namespace ServiceAnalyzer.core.Database;

public partial class MstmonitoraggioContext : DbContext
{
    private static string _connString = "";
    public MstmonitoraggioContext()
    {
    }

    public MstmonitoraggioContext(string connectionString)
    {
        _connString = connectionString;
    }

    public MstmonitoraggioContext(DbContextOptions<MstmonitoraggioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mstlog> Mstlogs { get; set; }

    public virtual DbSet<Mstservice> Mstservices { get; set; }

    public virtual DbSet<MstservicesConfig> MstservicesConfigs { get; set; }

    public virtual DbSet<MstservicesMethod> MstservicesMethods { get; set; }

    public virtual DbSet<MstservicesPolling> MstservicesPollings { get; set; }

    public virtual DbSet<MstlogIn> MstlogIns { get; set; }

    public virtual DbSet<ConfigurazioneAnalisiServizi> ConfigurazioneAnalisiServizis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstlogIn>(entity =>
        {
            entity.HasKey(e => e.IdLogIn);

            entity.ToTable("MSTLogIn");

            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ConfigurazioneAnalisiServizi>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<Mstlog>(entity =>
        {
            entity.HasKey(e => e.IdLog);

            entity.ToTable("MSTLog");

            entity.Property(e => e.Data).HasColumnType("datetime");
            entity.Property(e => e.Messaggio).IsUnicode(false);
            entity.Property(e => e.Metodo)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Mstservice>(entity =>
        {
            entity.HasKey(e => e.IdService);

            entity.ToTable("MSTServices");

            entity.Property(e => e.GuidServizio).IsUnicode(false);
            entity.Property(e => e.HostName).IsUnicode(false);
        });

        modelBuilder.Entity<MstservicesConfig>(entity =>
        {
            entity.HasKey(e => e.IdServiceConfig);

            entity.ToTable("MSTServicesConfig");

            entity.Property(e => e.ParName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ParValue)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MstservicesMethod>(entity =>
        {
            entity.HasKey(e => e.IdMetodo);

            entity.ToTable("MSTServicesMethods");

            entity.Property(e => e.Descrizione).IsUnicode(false);
        });

        modelBuilder.Entity<MstservicesPolling>(entity =>
        {
            entity.HasKey(e => e.IdServicesPolling);

            entity.ToTable("MSTServicesPolling");

            entity.Property(e => e.DataChiamata).HasColumnType("datetime");
            entity.Property(e => e.DataOraMailInviata).HasColumnType("datetime");
            entity.Property(e => e.Testo).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
