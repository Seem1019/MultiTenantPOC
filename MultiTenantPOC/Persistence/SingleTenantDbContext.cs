using Microsoft.EntityFrameworkCore;
using MultiTenant.Infraestructure.Interfaces;
using MultiTenantPOC.Models;

namespace MultiTenantPOC.Persistence
{
    public class SingleTenantDbContext : DbContext
    {
        private readonly MultiTenant.Infraestructure.Tenant _tenant;

        public SingleTenantDbContext(
            DbContextOptions<SingleTenantDbContext> options,
            ITenantAccessor<MultiTenant.Infraestructure.Tenant> tenantAccessor) : base(options)
        {
            _tenant = tenantAccessor.Tenant ?? throw new ArgumentNullException(nameof(MultiTenant.Infraestructure.Tenant));
        }

        public DbSet<Product> Products { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_tenant.Items["ConnectionString"]?.ToString());
        }
    }
}
