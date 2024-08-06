using Microsoft.EntityFrameworkCore;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Persistence.Interceptors;
using System.Reflection;

namespace Pacagroup.Ecommerce.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Discount>().ToTable("Discount");

            mb.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(mb);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
            optionsBuilder.EnableSensitiveDataLogging();
            //base.OnConfiguring(optionsBuilder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Discount> Discounts { get; set; }

    }

}