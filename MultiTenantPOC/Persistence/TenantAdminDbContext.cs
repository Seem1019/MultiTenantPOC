using Microsoft.EntityFrameworkCore;
using MultiTenantPOC.Models;

namespace MultiTenantPOC.Persistence { 

public class TenantAdminDbContext : DbContext
{
    public TenantAdminDbContext(DbContextOptions<TenantAdminDbContext> options)
        : base(options) { }

    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
}
