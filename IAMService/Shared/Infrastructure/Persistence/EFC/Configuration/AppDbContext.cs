using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using IAMService.IAM.Domain.Model.Aggregates;
using IAMService.Profiles.Domain.Model.Aggregate;
using IAMService.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IAMService.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
   protected override void OnConfiguring(DbContextOptionsBuilder builder)
   {
      //Para campos de auditor (CreatedDate, UpdatedDate)
      builder.AddCreatedUpdatedInterceptor();
      base.OnConfiguring(builder);
   }
   
   protected override void OnModelCreating(ModelBuilder builder)
   {
      base.OnModelCreating(builder);
      
      //=================================================================================================
      //||                                    CONFIGURATION OF THE TABLES                              ||                              
      //=================================================================================================
      
      //=================================================================================================
      //===================================== 1. GONZALO BOUNDED CONTEXT ===============================
      
      
      //---------------- CONFIGURATION DE PROFILES ----------------
      builder.Entity<Profile>().HasKey(p => p.Id);
      builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
      
      builder.Entity<Profile>().Property(p => p.CompanyName).IsRequired().HasMaxLength(50);
      builder.Entity<Profile>().Property(p => p.PhoneNumber).IsRequired().HasMaxLength(50);
      builder.Entity<Profile>().Property(p => p.RUC).IsRequired().HasMaxLength(50);
      
      builder.Entity<Profile>().OwnsOne(p => p.Name,
         n =>
         {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.FirstName).HasColumnName("FirstName");
            n.Property(p => p.LastName).HasColumnName("LastName");
         });

      builder.Entity<Profile>().OwnsOne(p => p.Email, e =>
      {
         e.WithOwner().HasForeignKey("Id");
         e.Property(a => a.Address).HasColumnName("EmailAddress");
      });

      builder.Entity<Profile>().OwnsOne(p => p.Address,
         a =>
         {
            a.WithOwner().HasForeignKey("Id");
            a.Property(s => s.Street).HasColumnName("AddressStreet");
            a.Property(s => s.Number).HasColumnName("AddressNumber");
            a.Property(s => s.City).HasColumnName("AddressCity");
            a.Property(s => s.Country).HasColumnName("AddressCountry");
         });
      
      // IAM Context
      builder.Entity<User>().HasKey(u => u.Id);
      builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
      builder.Entity<User>().Property(u => u.Username).IsRequired();
      builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
      
      // Relación uno a uno con la entidad Profile
      builder.Entity<User>()
         .HasOne(u => u.Profile)
         .WithOne(p => p.User)
         .HasForeignKey<Profile>(p => p.UserId)
         .OnDelete(DeleteBehavior.Cascade);

      
      
      
      
      //-----------------------------------------------------------------------------------------------
      //===================================== END GONZALO BOUNDED CONTEXT =============================
      //===============================================================================================
         
      
      
      //===================================== END VICENTE BOUNDED CONTEXT ===============================
      //=================================================================================================
   
      
      //Regals de mapped object relational (ORM)
      builder.UseSnakeCaseNamingConvention();
   }
}