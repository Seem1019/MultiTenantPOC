namespace MultiTenant.Infraestructure.Interfaces
{
public interface ITenantAccessor<T> where T : Tenant
{
    public T? Tenant { get; init; }
}
}