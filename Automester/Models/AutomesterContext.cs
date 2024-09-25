using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Automester.Models;

public partial class AutomesterContext : DbContext
{
    public AutomesterContext()
    {
    }

    public AutomesterContext(DbContextOptions<AutomesterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeRepair> EmployeeRepairs { get; set; }

    public virtual DbSet<ReadyForPickup> ReadyForPickups { get; set; }

    public virtual DbSet<Repair> Repairs { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Automester;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Car__68A0340EC24CAC63");

            entity.ToTable("Car");

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.RegistrationNumber).HasMaxLength(20);

            entity.HasOne(d => d.Customer).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Car_Customer");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8E4576192");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1456E7373");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Position).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeRepair>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.RepairId }).HasName("PK__Employee__BAAD442DD9514CAE");

            entity.ToTable("EmployeeRepair");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.RepairId).HasColumnName("RepairID");
            entity.Property(e => e.HoursWorked).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeRepairs)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeRepair_Employee");

            entity.HasOne(d => d.Repair).WithMany(p => p.EmployeeRepairs)
                .HasForeignKey(d => d.RepairId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeRepair_Repair");
        });

        modelBuilder.Entity<ReadyForPickup>(entity =>
        {
            entity.HasKey(e => e.PickupId).HasName("PK__ReadyFor__310DAFCF7B743451");

            entity.ToTable("ReadyForPickup");

            entity.Property(e => e.PickupId).HasColumnName("PickupID");
            entity.Property(e => e.CarId).HasColumnName("CarID");

            entity.HasOne(d => d.Car).WithMany(p => p.ReadyForPickups)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK_ReadyForPickup_Car");
        });

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasKey(e => e.RepairId).HasName("PK__Repair__07D0BDCD2C2FE679");

            entity.ToTable("Repair", tb =>
                {
                    tb.HasTrigger("trigger_add_to_workshop");
                    tb.HasTrigger("trigger_move_to_ready_for_pickup");
                });

            entity.Property(e => e.RepairId).HasColumnName("RepairID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Car).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK_Repair_Car");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.WorkshopId).HasName("PK__Workshop__7A008C2A8E1846A6");

            entity.ToTable("Workshop");

            entity.Property(e => e.WorkshopId).HasColumnName("WorkshopID");
            entity.Property(e => e.CarId).HasColumnName("CarID");

            entity.HasOne(d => d.Car).WithMany(p => p.Workshops)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK_Workshop_Car");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
