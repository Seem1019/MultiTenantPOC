
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MultiTenant.Infraestructure;
using MultiTenant.Infraestructure.Interfaces;
using MultiTenantPOC.Persistence;


namespace MultiTenantPOC.MultiTenancy { 
public class DbContextTenantStore : ITenantStore<Tenant>
{
    private readonly TenantAdminDbContext _context;
    private readonly IMemoryCache _cache;

    public DbContextTenantStore(TenantAdminDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

        public async Task<Tenant> GetTenantAsync(string identifier)
        {
            var cacheKey = $"Cache_{identifier}";
            var tenant = _cache.Get<Tenant>(cacheKey);

            if (tenant is null)
            {
                var entity = await _context.Tenants.Where(b => b.Identifier == identifier).FirstOrDefaultAsync();

                tenant = new Tenant(entity.TenantId, entity.Identifier);

                tenant.Items["Name"] = entity.Name;
                tenant.Items["ConnectionString"] = entity.ConnectionString; 

                _cache.Set(cacheKey, tenant);
            }

            return tenant;
        }
    }
}