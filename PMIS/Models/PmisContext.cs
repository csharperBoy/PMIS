using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<ClaimOnSystem> ClaimOnSystems { get; set; }

    public virtual DbSet<ClaimUserOnIndicator> ClaimUserOnIndicators { get; set; }

    public virtual DbSet<Indicator> Indicators { get; set; }

    public virtual DbSet<IndicatorCategory> IndicatorCategories { get; set; }

    public virtual DbSet<IndicatorValue> IndicatorValues { get; set; }

    public virtual DbSet<LookUp> LookUps { get; set; }

    public virtual DbSet<LookUpDestination> LookUpDestinations { get; set; }

    public virtual DbSet<LookUpValue> LookUpValues { get; set; }

    public virtual DbSet<SerilogTable> SerilogTables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = 192.168.3.64; Database = PMIS; User Id = devlogin; Password = Fajr@123;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClaimOnSystem>(entity =>
        {
            entity.ToTable("ClaimOnSystem");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.FkLkpClaimOnSystem).WithMany(p => p.ClaimOnSystems)
                .HasForeignKey(d => d.FkLkpClaimOnSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClaimOnSystem_LookUpValue");

            entity.HasOne(d => d.FkUser).WithMany(p => p.ClaimOnSystems)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClaimOnSystem_User");
        });

        modelBuilder.Entity<ClaimUserOnIndicator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Claim");

            entity.ToTable("ClaimUserOnIndicator");

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

        modelBuilder.Entity<Indicator>(entity =>
        {
            entity.ToTable("Indicator");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code).IsUnicode(false);
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

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ColumnName).IsUnicode(false);
            entity.Property(e => e.FkLookUpId).HasColumnName("FkLookUpID");
            entity.Property(e => e.TableName).IsUnicode(false);

            entity.HasOne(d => d.FkLookUp).WithMany(p => p.LookUpDestinations)
                .HasForeignKey(d => d.FkLookUpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LookUpDestination_LookUp");
        });

        modelBuilder.Entity<LookUpValue>(entity =>
        {
            entity.ToTable("LookUpValue");

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

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PasswordHash).IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserName).IsUnicode(false);

            entity.HasOne(d => d.FkLkpWorkCalendar).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkLkpWorkCalendarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_LookUpValue");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
