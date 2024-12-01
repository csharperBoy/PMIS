using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PMIS.Models;

public partial class PmisContext : DbContext
{
    public PmisContext()
    {
    }

    public PmisContext(DbContextOptions<PmisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClaimUserOnIndicator> ClaimUserOnIndicators { get; set; }

    public virtual DbSet<ClaimUserOnSystem> ClaimUserOnSystems { get; set; }

    public virtual DbSet<Indicator> Indicators { get; set; }

    public virtual DbSet<IndicatorCategory> IndicatorCategories { get; set; }

    public virtual DbSet<IndicatorValue> IndicatorValues { get; set; }

    public virtual DbSet<LookUp> LookUps { get; set; }

    public virtual DbSet<LookUpDestination> LookUpDestinations { get; set; }

    public virtual DbSet<LookUpValue> LookUpValues { get; set; }

    public virtual DbSet<SerilogTable> SerilogTables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        if (!optionsBuilder.IsConfigured)
        {
            string? connectionString = configuration.GetSection("ConnectionStrings:PmisConnectionString").Value;
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    //=> optionsBuilder.UseSqlServer("Server = 192.168.3.64; Database = PMIS; User Id = devlogin; Password = Fajr@123;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClaimUserOnIndicator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Claim");

            entity.ToTable("ClaimUserOnIndicator");

            entity.HasIndex(e => e.FkLkpClaimUserOnIndicatorId, "IX_ClaimUserOnIndicator_FkLkpClaimUserOnIndicatorID");

            entity.HasIndex(e => e.FkUserId, "IX_ClaimUserOnIndicator_FkUserID");

            entity.HasIndex(e => new { e.FkIndicatorId, e.FkLkpClaimUserOnIndicatorId, e.FkUserId }, "UNQ_ClaimUserIndicator").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkIndicatorId).HasColumnName("FkIndicatorID");
            entity.Property(e => e.FkLkpClaimUserOnIndicatorId).HasColumnName("FkLkpClaimUserOnIndicatorID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");

            entity.HasOne(d => d.FkIndicator).WithMany(p => p.ClaimUserOnIndicators)
                .HasForeignKey(d => d.FkIndicatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClaimUserOnIndicator_Indicator");

            entity.HasOne(d => d.FkLkpClaimUserOnIndicator).WithMany(p => p.ClaimUserOnIndicators)
                .HasForeignKey(d => d.FkLkpClaimUserOnIndicatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClaimUserOnIndicator_LookUpValue");

            entity.HasOne(d => d.FkUser).WithMany(p => p.ClaimUserOnIndicators)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClaimUserOnIndicator_User");
        });

        modelBuilder.Entity<ClaimUserOnSystem>(entity =>
        {
            entity.ToTable("ClaimUserOnSystem");

            entity.HasIndex(e => e.FkUserId, "IX_ClaimUserOnSystem_FkUserId");

            entity.HasIndex(e => new { e.FkLkpClaimUserOnSystemId, e.FkUserId }, "UNQ_UserClaim").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.FkLkpClaimUserOnSystem).WithMany(p => p.ClaimUserOnSystems)
                .HasForeignKey(d => d.FkLkpClaimUserOnSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClaimUserOnSystem_LookUpValue");

            entity.HasOne(d => d.FkUser).WithMany(p => p.ClaimUserOnSystems)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClaimUserOnSystem_User");
        });

        modelBuilder.Entity<Indicator>(entity =>
        {
            entity.ToTable("Indicator");

            entity.HasIndex(e => e.FkLkpDesirabilityId, "IX_Indicator_FkLkpDesirabilityID");

            entity.HasIndex(e => e.FkLkpFormId, "IX_Indicator_FkLkpFormID");

            entity.HasIndex(e => e.FkLkpManualityId, "IX_Indicator_FkLkpManualityID");

            entity.HasIndex(e => e.FkLkpMeasureId, "IX_Indicator_FkLkpMeasureID");

            entity.HasIndex(e => e.FkLkpPeriodId, "IX_Indicator_FkLkpPeriodID");

            entity.HasIndex(e => e.FkLkpUnitId, "IX_Indicator_FkLkpUnitID");

            entity.HasIndex(e => e.Code, "UNQ_Code").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FkLkpDesirabilityId).HasColumnName("FkLkpDesirabilityID");
            entity.Property(e => e.FkLkpFormId).HasColumnName("FkLkpFormID");
            entity.Property(e => e.FkLkpManualityId).HasColumnName("FkLkpManualityID");
            entity.Property(e => e.FkLkpMeasureId).HasColumnName("FkLkpMeasureID");
            entity.Property(e => e.FkLkpPeriodId).HasColumnName("FkLkpPeriodID");
            entity.Property(e => e.FkLkpUnitId).HasColumnName("FkLkpUnitID");

            entity.HasOne(d => d.FkLkpDesirability).WithMany(p => p.IndicatorFkLkpDesirabilities)
                .HasForeignKey(d => d.FkLkpDesirabilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Indicator_LookUpValue_Desirability");

            entity.HasOne(d => d.FkLkpForm).WithMany(p => p.IndicatorFkLkpForms)
                .HasForeignKey(d => d.FkLkpFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Indicator_LookUpValue_Form");

            entity.HasOne(d => d.FkLkpManuality).WithMany(p => p.IndicatorFkLkpManualities)
                .HasForeignKey(d => d.FkLkpManualityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Indicator_LookUpValue_Manuality");

            entity.HasOne(d => d.FkLkpMeasure).WithMany(p => p.IndicatorFkLkpMeasures)
                .HasForeignKey(d => d.FkLkpMeasureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Indicator_LookUpValue_Measure");

            entity.HasOne(d => d.FkLkpPeriod).WithMany(p => p.IndicatorFkLkpPeriods)
                .HasForeignKey(d => d.FkLkpPeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Indicator_LookUpValue_Period");

            entity.HasOne(d => d.FkLkpUnit).WithMany(p => p.IndicatorFkLkpUnits)
                .HasForeignKey(d => d.FkLkpUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Indicator_LookUpValue_Unit");
        });

        modelBuilder.Entity<IndicatorCategory>(entity =>
        {
            entity.ToTable("IndicatorCategory");

            entity.HasIndex(e => e.FkIndicatorId, "IX_IndicatorCategory_FkIndicatorID");

            entity.HasIndex(e => e.FkLkpCategoryDetailId, "IX_IndicatorCategory_FkLkpCategoryDetailID");

            entity.HasIndex(e => e.FkLkpCategoryMasterId, "IX_IndicatorCategory_FkLkpCategoryMasterID");

            entity.HasIndex(e => e.FkLkpCategoryTypeId, "IX_IndicatorCategory_FkLkpCategoryTypeID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkIndicatorId).HasColumnName("FkIndicatorID");
            entity.Property(e => e.FkLkpCategoryDetailId).HasColumnName("FkLkpCategoryDetailID");
            entity.Property(e => e.FkLkpCategoryMasterId).HasColumnName("FkLkpCategoryMasterID");
            entity.Property(e => e.FkLkpCategoryTypeId).HasColumnName("FkLkpCategoryTypeID");

            entity.HasOne(d => d.FkIndicator).WithMany(p => p.IndicatorCategories)
                .HasForeignKey(d => d.FkIndicatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndicatorCategory_Indicator");

            entity.HasOne(d => d.FkLkpCategoryDetail).WithMany(p => p.IndicatorCategoryFkLkpCategoryDetails)
                .HasForeignKey(d => d.FkLkpCategoryDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndicatorCategory_LookUpValue_Detail");

            entity.HasOne(d => d.FkLkpCategoryMaster).WithMany(p => p.IndicatorCategoryFkLkpCategoryMasters)
                .HasForeignKey(d => d.FkLkpCategoryMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndicatorCategory_LookUpValue_Master");

            entity.HasOne(d => d.FkLkpCategoryType).WithMany(p => p.IndicatorCategoryFkLkpCategoryTypes)
                .HasForeignKey(d => d.FkLkpCategoryTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndicatorCategory_LookUpValue_Type");
        });

        modelBuilder.Entity<IndicatorValue>(entity =>
        {
            entity.ToTable("IndicatorValue");

            entity.HasIndex(e => e.FkIndicatorId, "IX_IndicatorValue_FkIndicatorID");

            entity.HasIndex(e => e.FkLkpShiftId, "IX_IndicatorValue_FkLkpShiftID");

            entity.HasIndex(e => e.FkLkpValueTypeId, "IX_IndicatorValue_FkLkpValueTypeID");

            entity.HasIndex(e => new { e.DateTime, e.FkIndicatorId, e.FkLkpShiftId, e.FkLkpValueTypeId }, "UNQ_Key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkIndicatorId).HasColumnName("FkIndicatorID");
            entity.Property(e => e.FkLkpShiftId).HasColumnName("FkLkpShiftID");
            entity.Property(e => e.FkLkpValueTypeId).HasColumnName("FkLkpValueTypeID");
            entity.Property(e => e.Value).HasColumnType("decimal(24, 6)");

            entity.HasOne(d => d.FkIndicator).WithMany(p => p.IndicatorValues)
                .HasForeignKey(d => d.FkIndicatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndicatorValue_Indicator");

            entity.HasOne(d => d.FkLkpShift).WithMany(p => p.IndicatorValueFkLkpShifts)
                .HasForeignKey(d => d.FkLkpShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndicatorValue_LookUpValue_Shift");

            entity.HasOne(d => d.FkLkpValueType).WithMany(p => p.IndicatorValueFkLkpValueTypes)
                .HasForeignKey(d => d.FkLkpValueTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndicatorValue_LookUpValue_Type");
        });

        modelBuilder.Entity<LookUp>(entity =>
        {
            entity.ToTable("LookUp");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<LookUpDestination>(entity =>
        {
            entity.ToTable("LookUpDestination");

            entity.HasIndex(e => e.FkLookUpId, "IX_LookUpDestination_FkLookUpID");

            entity.HasIndex(e => new { e.ColumnName, e.TableName, e.FkLookUpId }, "UNQ_LookUpDestination").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ColumnName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.FkLookUpId).HasColumnName("FkLookUpID");
            entity.Property(e => e.TableName)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.FkLookUp).WithMany(p => p.LookUpDestinations)
                .HasForeignKey(d => d.FkLookUpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LookUpDestination_LookUp");
        });

        modelBuilder.Entity<LookUpValue>(entity =>
        {
            entity.ToTable("LookUpValue");

            entity.HasIndex(e => e.FkLookUpId, "IX_LookUpValue_FkLookUpID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkLookUpId).HasColumnName("FkLookUpID");

            entity.HasOne(d => d.FkLookUp).WithMany(p => p.LookUpValues)
                .HasForeignKey(d => d.FkLookUpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LookUpValue_LookUp");
        });

        modelBuilder.Entity<SerilogTable>(entity =>
        {
            entity.ToTable("SerilogTable");

            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.UserName, "UNQ_UserName").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PasswordHash).IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.FkLkpWorkCalendar).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkLkpWorkCalendarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_LookUpValue");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
