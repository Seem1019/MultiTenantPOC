namespace MultiTenant.Infraestructure.Interfaces
{

    public interface ITenantResolutionStrategy
    {
        Task<string> GetTenantIdentifierAsync();
    }
}
