using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Weather.Dal.Models;

public partial class LigadatabaseContext : DbContext
{
    public LigadatabaseContext()
    {
    }

    public LigadatabaseContext(DbContextOptions<LigadatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<WeatherDaily> WeatherDailies { get; set; }

    public virtual DbSet<WeatherDayDetail> WeatherDayDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Database=LIGADATABASE;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherDaily>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WeatherD__3214EC07A49686A4");

            entity.ToTable("WeatherDaily");

            entity.HasIndex(e => e.Day, "UQ__WeatherD__C0301F12977A65E6").IsUnique();

            entity.HasMany(c => c.WeatherDayDetails)
                  .WithOne(e => e.Day)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<WeatherDayDetail>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => new { e.DayId, e.Time }, "UC_DayTime").IsUnique();

            entity.HasOne(d => d.Day).WithMany()
                .HasForeignKey(d => d.DayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WeatherDa__DayId__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
