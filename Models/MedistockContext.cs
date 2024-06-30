using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MedistockApp.Models;

public partial class MedistockContext : DbContext
{
    public MedistockContext()
    {
    }

    public MedistockContext(DbContextOptions<MedistockContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appoinment> Appoinments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Medicinesprescription> Medicinesprescriptions { get; set; }

    public virtual DbSet<Medicinessupplier> Medicinessuppliers { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Scheduling> Schedulings { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//       => optionsBuilder.UseMySql("server=localhost;port=3306;database=medistock;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Appoinment>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("PRIMARY");

            entity.ToTable("appoinments");

            entity.HasIndex(e => e.FkIdScheduling, "fkIdScheduling");

            entity.Property(e => e.IdAppointment)
                .HasColumnType("int(11)")
                .HasColumnName("idAppointment");
            entity.Property(e => e.DateHour)
                .HasMaxLength(20)
                .HasColumnName("dateHour");
            entity.Property(e => e.FkIdScheduling)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdScheduling");

            entity.HasOne(d => d.FkIdSchedulingNavigation).WithMany(p => p.Appoinments)
                .HasForeignKey(d => d.FkIdScheduling)
                .HasConstraintName("appoinments_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.IdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("idCategory");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.IdDiagnosis).HasName("PRIMARY");

            entity.ToTable("diagnosis");

            entity.HasIndex(e => e.FkIdAppointment, "fkIdAppointment");

            entity.Property(e => e.IdDiagnosis)
                .HasColumnType("int(11)")
                .HasColumnName("idDiagnosis");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.FkIdAppointment)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdAppointment");

            entity.HasOne(d => d.FkIdAppointmentNavigation).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.FkIdAppointment)
                .HasConstraintName("diagnosis_ibfk_1");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.IdMedicine).HasName("PRIMARY");

            entity.ToTable("medicines");

            entity.HasIndex(e => e.FkIdCategory, "fkIdCategory");

            entity.Property(e => e.IdMedicine)
                .HasColumnType("int(11)")
                .HasColumnName("idMedicine");
            entity.Property(e => e.ExpirationDate)
                .HasMaxLength(10)
                .HasColumnName("expirationDate");
            entity.Property(e => e.FkIdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdCategory");
            entity.Property(e => e.Format)
                .HasMaxLength(50)
                .HasColumnName("format");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Stock)
                .HasColumnType("int(11)")
                .HasColumnName("stock");

            entity.HasOne(d => d.FkIdCategoryNavigation).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.FkIdCategory)
                .HasConstraintName("medicines_ibfk_1");
        });

        modelBuilder.Entity<Medicinesprescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("medicinesprescriptions");

            entity.HasIndex(e => e.FkIdMedicine, "fkIdMedicine");

            entity.HasIndex(e => e.FkIdPrescription, "fkIdPrescription");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("int(11)")
                .HasColumnName("amount");
            entity.Property(e => e.FkIdMedicine)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdMedicine");
            entity.Property(e => e.FkIdPrescription)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdPrescription");

            entity.HasOne(d => d.FkIdMedicineNavigation).WithMany(p => p.Medicinesprescriptions)
                .HasForeignKey(d => d.FkIdMedicine)
                .HasConstraintName("medicinesprescriptions_ibfk_1");

            entity.HasOne(d => d.FkIdPrescriptionNavigation).WithMany(p => p.Medicinesprescriptions)
                .HasForeignKey(d => d.FkIdPrescription)
                .HasConstraintName("medicinesprescriptions_ibfk_2");
        });

        modelBuilder.Entity<Medicinessupplier>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("medicinessuppliers");

            entity.HasIndex(e => e.FkIdMedicine, "fkIdMedicine");

            entity.HasIndex(e => e.FkIdSupplier, "fkIdSupplier");

            entity.Property(e => e.FkIdMedicine)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdMedicine");
            entity.Property(e => e.FkIdSupplier)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdSupplier");

            entity.HasOne(d => d.FkIdMedicineNavigation).WithMany()
                .HasForeignKey(d => d.FkIdMedicine)
                .HasConstraintName("medicinessuppliers_ibfk_1");

            entity.HasOne(d => d.FkIdSupplierNavigation).WithMany()
                .HasForeignKey(d => d.FkIdSupplier)
                .HasConstraintName("medicinessuppliers_ibfk_2");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.IdPrescription).HasName("PRIMARY");

            entity.ToTable("prescriptions");

            entity.HasIndex(e => e.FkIdDiagnosis, "fkIdDiagnosis");

            entity.Property(e => e.IdPrescription)
                .HasColumnType("int(11)")
                .HasColumnName("idPrescription");
            entity.Property(e => e.FkIdDiagnosis)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdDiagnosis");
            entity.Property(e => e.Medicines)
                .HasMaxLength(500)
                .HasColumnName("medicines");

            entity.HasOne(d => d.FkIdDiagnosisNavigation).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.FkIdDiagnosis)
                .HasConstraintName("prescriptions_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRole)
                .HasColumnType("int(11)")
                .HasColumnName("idRole");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Scheduling>(entity =>
        {
            entity.HasKey(e => e.IdScheduling).HasName("PRIMARY");

            entity.ToTable("schedulings");

            entity.HasIndex(e => e.FkIdDoctor, "fkIdDoctor");

            entity.HasIndex(e => e.FkIdPatient, "fkIdPatient");

            entity.Property(e => e.IdScheduling)
                .HasColumnType("int(11)")
                .HasColumnName("idScheduling");
            entity.Property(e => e.FkIdDoctor)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdDoctor");
            entity.Property(e => e.FkIdPatient)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdPatient");
            entity.Property(e => e.Reason)
                .HasMaxLength(100)
                .HasColumnName("reason");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");

            entity.HasOne(d => d.FkIdDoctorNavigation).WithMany(p => p.SchedulingFkIdDoctorNavigations)
                .HasForeignKey(d => d.FkIdDoctor)
                .HasConstraintName("schedulings_ibfk_2");

            entity.HasOne(d => d.FkIdPatientNavigation).WithMany(p => p.SchedulingFkIdPatientNavigations)
                .HasForeignKey(d => d.FkIdPatient)
                .HasConstraintName("schedulings_ibfk_1");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.IdSupplier).HasName("PRIMARY");

            entity.ToTable("suppliers");

            entity.Property(e => e.IdSupplier)
                .HasColumnType("int(11)")
                .HasColumnName("idSupplier");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.FkIdRole, "fkIdRole");

            entity.Property(e => e.IdUser)
                .HasColumnType("int(11)")
                .HasColumnName("idUser");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Age)
                .HasColumnType("int(11)")
                .HasColumnName("age");
            entity.Property(e => e.Birthdate)
                .HasMaxLength(10)
                .HasColumnName("birthdate");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(20)
                .HasColumnName("documentType");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FkIdRole)
                .HasColumnType("int(11)")
                .HasColumnName("fkIdRole");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Profession)
                .HasMaxLength(50)
                .HasColumnName("profession");

            entity.HasOne(d => d.FkIdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkIdRole)
                .HasConstraintName("users_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
