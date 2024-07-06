using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JJK.Entities;

public partial class UrsalcoopContext : DbContext
{
    public UrsalcoopContext()
    {
    }

    public UrsalcoopContext(DbContextOptions<UrsalcoopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientsInfoTb> ClientsInfoTbs { get; set; }

    public virtual DbSet<LoanDb> LoanDbs { get; set; }

    public virtual DbSet<LoginTb> LoginTbs { get; set; }

    public virtual DbSet<PaymentsTb> PaymentsTbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;database=ursalcoop;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<ClientsInfoTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("clients_info_tb")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(50)")
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Age).HasColumnType("int(50)");
            entity.Property(e => e.Birthday).HasMaxLength(100);
            entity.Property(e => e.CivilStatus)
                .HasMaxLength(100)
                .HasColumnName("Civil_Status");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("Full_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("Last_Name");
            entity.Property(e => e.NameOfFather).HasMaxLength(255);
            entity.Property(e => e.NameOfMother).HasMaxLength(255);
            entity.Property(e => e.Occupation).HasMaxLength(100);
            entity.Property(e => e.Religion).HasMaxLength(100);
            entity.Property(e => e.UserType)
                .HasMaxLength(100)
                .HasColumnName("User_Type");
            entity.Property(e => e.ZipCode)
                .HasColumnType("int(100)")
                .HasColumnName("ZIP_Code");
        });

        modelBuilder.Entity<LoanDb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("loan_db")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id).HasColumnType("int(50)");
            entity.Property(e => e.Amount).HasColumnType("int(50)");
            entity.Property(e => e.ClientId).HasColumnType("int(10)");
            entity.Property(e => e.DateCreated).HasColumnName("Date_Created");
            entity.Property(e => e.Deduction).HasPrecision(10);
            entity.Property(e => e.DueDate).HasColumnName("Due_Date");
            entity.Property(e => e.Interest).HasPrecision(10);
            entity.Property(e => e.NoOfPayment)
                .HasColumnType("int(10)")
                .HasColumnName("No_Of_Payment");
            entity.Property(e => e.PayableAmount)
                .HasPrecision(10)
                .HasColumnName("Payable_amount");
            entity.Property(e => e.RecievableAmount)
                .HasPrecision(10)
                .HasColumnName("Recievable_Amount");
            entity.Property(e => e.Status).HasMaxLength(200);
            entity.Property(e => e.Term).HasColumnType("int(10)");
            entity.Property(e => e.Type).HasMaxLength(200);
        });

        modelBuilder.Entity<LoginTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("login_tb")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<PaymentsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("payments_tb")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(50)")
                .HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasColumnType("int(50)")
                .HasColumnName("client_id");
            entity.Property(e => e.Collectable)
                .HasPrecision(10)
                .HasColumnName("collectable");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Deduction)
                .HasPrecision(10)
                .HasColumnName("deduction");
            entity.Property(e => e.LoanId)
                .HasColumnType("int(50)")
                .HasColumnName("loan_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
