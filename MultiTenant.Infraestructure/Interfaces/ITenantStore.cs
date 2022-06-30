

namespace MultiTenant.Infraestructure.Interfaces
{
    public interface ITenantStore<T> where T : Tenant
    {
        Task<T> GetTenantAsync(string identifier);
    }
}
