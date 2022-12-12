using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Require.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Require.Data
{
    public class RequireDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Clause> Clauses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<ClauseType> Types { get; set; }

        public RequireDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(r => r.Id);

            modelBuilder.Entity<ClauseType>().HasKey(r => r.Id);
            modelBuilder.Entity<ClauseType>().HasOne(r => r.Cabinet).WithMany().HasForeignKey(r => r.CabinetId);
            modelBuilder.Entity<ClauseType>().HasIndex(r => new { r.Tag, r.CabinetId });

            modelBuilder.Entity<Member>().HasKey(r => r.Id);
            modelBuilder.Entity<Member>().HasOne(r => r.Cabinet).WithMany().HasForeignKey(r => r.CabinetId);
            modelBuilder.Entity<Member>().HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
            modelBuilder.Entity<Member>().HasIndex(r => r.InvitedAddress);
            modelBuilder.Entity<Member>().HasIndex(r => new { r.CabinetId, r.UserId });

            modelBuilder.Entity<Document>().HasKey(r => r.Id);

            modelBuilder.Entity<Clause>().HasKey(r => r.Id);
            modelBuilder.Entity<Clause>().HasOne(r => r.Document).WithMany().HasForeignKey(r => r.DocumentId);
            modelBuilder.Entity<Clause>().HasIndex(r => new { r.DocumentId, r.DocumentOrder });
            modelBuilder.Entity<Clause>().HasOne(r => r.Type).WithMany().HasForeignKey(r => r.TypeId);
            modelBuilder.Entity<Clause>().OwnsOne(
                clause => clause.PropertyValues,
                ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                    ownedNavigationBuilder.OwnsMany(propertyValues => propertyValues.Values);
                }
                );
            modelBuilder.Entity<Clause>().HasOne(r => r.ParentClause).WithMany().HasForeignKey(r => r.ParentClauseId);

            modelBuilder.Entity<Property>().HasKey(r => r.Id);
            modelBuilder.Entity<Property>().HasOne(r => r.Type).WithMany().HasForeignKey(r => r.TypeId);
            modelBuilder.Entity<Property>().OwnsOne(
                property => property.PropertyMeta,
                ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                    ownedNavigationBuilder.OwnsOne(r => r.Select, selectNavigationBuilder =>
                    {
                        selectNavigationBuilder.OwnsMany(r => r.Values);
                    });
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}