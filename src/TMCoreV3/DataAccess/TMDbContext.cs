﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMCoreV3.ViewModels;
using TMCoreV3.DataAccess.Models.User;
using TMCoreV3.DataAccess.Models.Customer;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TMCoreV3.DataAccess
{
    public class TMDbContext : IdentityDbContext<AuthUser,AuthRole,string>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerApplianceProblem> CustomerApplianceProblems { get; set; }
        public DbSet<CustomerApplianceType> CustomerApplianceTypes { get; set; }
        public DbSet<CustomerApplianceBrand> CustomerApplianceBrands { get; set; }
        public DbSet<CustomerCoupon> CustomerCoupons { get; set; }
        public TMDbContext(DbContextOptions<TMDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CustomerApplianceProblem>()
                .HasOne(p => p.Customer)
                .WithMany(p => p.CustomerApplianceProblems)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("FK_CustomerApplianceProblem_Customer")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CustomerApplianceBrand>()
                .HasOne(p => p.customerApplianceType)
                .WithMany(p => p.CustomerApplianceBrands)
                .HasForeignKey(p => p.CustomerApplianceTypeId)
                .HasConstraintName("FK_CustomerApplianceBrand_Customer")
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<CustomerApplianceProblem>()
            //    .HasOne(p => p.CustomerApplianceType);


            //builder.Entity<CustomerApplianceProblem>()
            //    .HasOne(p => p.CustomerApplianceBrand);

            //builder.Entity<CustomerApplianceProblem>()
            //    .HasOne(c => c.CustomerApplianceType)                
            //    .WithOne()
            //    .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            //builder.Entity<CustomerApplianceProblem>()
            //    .HasOne(c => c.CustomerApplianceBrand)
            //    .WithOne()
            //    .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            //builder.Entity<CustomerApplianceType>()
            //  .HasMany(c => c.CustomerApplianceBrands)
            //  .WithOne()
            //  .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            //builder.Entity<CustomerApplianceProblem>()
            //   .HasOne(c => c.customerApplianceType)
            //   .WithOne()
            //   .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

            //builder.Entity<CustomerApplianceProblem>()
            //   .HasOne(c => c.customerApplianceBrand)
            //   .WithOne()
            //   .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);


        }
    }
}
