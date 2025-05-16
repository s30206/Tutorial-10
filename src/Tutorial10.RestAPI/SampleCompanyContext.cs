using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tutorial10.RestAPI;

public partial class SampleCompanyContext : DbContext
{
    public SampleCompanyContext()
    {
    }

    public SampleCompanyContext(DbContextOptions<SampleCompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departemnt> Departemnts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("s30206");

        modelBuilder.Entity<Departemnt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departem__3214EC07A8A2B7E5");

            entity.ToTable("Departemnt");

            entity.HasIndex(e => e.Name, "UQ__Departem__737584F62346E739").IsUnique();

            entity.Property(e => e.Location)
                .HasMaxLength(33)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0749010139");

            entity.ToTable("Employee");

            entity.Property(e => e.Commission).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Depart__23578376");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__JobId__216F3B04");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Employee__Manage__22635F3D");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Job__3214EC07FB894B02");

            entity.ToTable("Job");

            entity.HasIndex(e => e.Name, "UQ__Job__737584F6B40AA0C9").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
