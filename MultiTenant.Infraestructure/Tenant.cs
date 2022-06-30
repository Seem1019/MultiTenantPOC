namespace MultiTenant.Infraestructure
{
    public record Tenant(int Id, string Identifier)
    {
        public Dictionary<string, object> Items { get; init; } =
            new Dictionary<string, object>();
    }

}