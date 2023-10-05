﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sistema_Tec_Web_API.Models;

public partial class Sistema_TecContext : DbContext
{
    public Sistema_TecContext(DbContextOptions<Sistema_TecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Degree> Degrees { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationType> OrganizationTypes { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Applicat__3213E83F012A5EE0");

            entity.ToTable("Application");

            entity.Property(e => e.applicationName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.description)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ApplicationRole>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Applicat__3213E83F8968BC1C");

            entity.ToTable("ApplicationRole");

            entity.Property(e => e.applicationRoleName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.application).WithMany(p => p.ApplicationRoles)
                .HasForeignKey(d => d.applicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__appli__4222D4EF");

            entity.HasOne(d => d.parent).WithMany(p => p.Inverseparent)
                .HasForeignKey(d => d.parentId)
                .HasConstraintName("FK__Applicati__paren__4316F928");
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Campus__3213E83F508710FB");

            entity.ToTable("Campus");

            entity.Property(e => e.campusName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Degree>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Degree__3213E83F1680A5DC");

            entity.ToTable("Degree");

            entity.Property(e => e.degreeName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.campus).WithMany(p => p.Degrees)
                .HasForeignKey(d => d.campusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Degree__campusId__267ABA7A");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Departme__3213E83FC175A167");

            entity.ToTable("Department");

            entity.Property(e => e.departmentName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.campus).WithMany(p => p.Departments)
                .HasForeignKey(d => d.campusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Departmen__campu__2C3393D0");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Employee__3213E83F7616F889");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.email, "UQ__Employee__AB6E6164FD296061").IsUnique();

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.emailNavigation).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__email__37A5467C");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.email).HasName("PK__Organiza__AB6E6165DD3607BB");

            entity.ToTable("Organization");

            entity.Property(e => e.email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.organizationName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.organizationPassword)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.degree).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.degreeId)
                .HasConstraintName("FK__Organizat__degre__3C69FB99");

            entity.HasOne(d => d.organizationType).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.organizationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Organizat__organ__3D5E1FD2");
        });

        modelBuilder.Entity<OrganizationType>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Organiza__3213E83F15234458");

            entity.ToTable("OrganizationType");

            entity.Property(e => e.organizationTypeName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.email).HasName("PK__Person__AB6E61658A0294FA");

            entity.ToTable("Person");

            entity.HasIndex(e => e.id, "UQ__Person__3213E83E526F3A9F").IsUnique();

            entity.Property(e => e.email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.firstLastName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.personName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.personPassword)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.secondLastName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasMany(d => d.applicationRoles).WithMany(p => p.emails)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonXApplicationRole",
                    r => r.HasOne<ApplicationRole>().WithMany()
                        .HasForeignKey("applicationRoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PersonXAp__appli__46E78A0C"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("email")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PersonXAp__email__45F365D3"),
                    j =>
                    {
                        j.HasKey("email", "applicationRoleId").HasName("PK__PersonXA__1ADDA5278C7B7992");
                        j.ToTable("PersonXApplicationRole");
                        j.IndexerProperty<string>("email")
                            .HasMaxLength(255)
                            .IsUnicode(false);
                    });

            entity.HasMany(d => d.departments).WithMany(p => p.emails)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonXDepartment",
                    r => r.HasOne<Department>().WithMany()
                        .HasForeignKey("departmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PersonXDe__depar__4E88ABD4"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("email")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PersonXDe__email__4D94879B"),
                    j =>
                    {
                        j.HasKey("email", "departmentId").HasName("PK__PersonXD__64F5E2239C0C30BF");
                        j.ToTable("PersonXDepartment");
                        j.IndexerProperty<string>("email")
                            .HasMaxLength(255)
                            .IsUnicode(false);
                    });

            entity.HasMany(d => d.schools).WithMany(p => p.emails)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonXSchool",
                    r => r.HasOne<School>().WithMany()
                        .HasForeignKey("schoolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PersonXSc__schoo__4AB81AF0"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("email")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PersonXSc__email__49C3F6B7"),
                    j =>
                    {
                        j.HasKey("email", "schoolId").HasName("PK__PersonXS__2A47D81C930AC1B6");
                        j.ToTable("PersonXSchool");
                        j.IndexerProperty<string>("email")
                            .HasMaxLength(255)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__School__3213E83F51386067");

            entity.ToTable("School");

            entity.Property(e => e.schoolName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.campus).WithMany(p => p.Schools)
                .HasForeignKey(d => d.campusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__School__campusId__29572725");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Student__3213E83FAD6627F0");

            entity.ToTable("Student");

            entity.HasIndex(e => e.email, "UQ__Student__AB6E6164E99F614B").IsUnique();

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.degree).WithMany(p => p.Students)
                .HasForeignKey(d => d.degreeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__degreeI__33D4B598");

            entity.HasOne(d => d.emailNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__email__32E0915F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}