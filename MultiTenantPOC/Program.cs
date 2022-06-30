using Microsoft.EntityFrameworkCore;
using MultiTenant.Infraestructure;
using MultiTenant.Infraestructure.Extensions;
using MultiTenant.Infraestructure.Interfaces;
using MultiTenantPOC.MultiTenancy;
using MultiTenantPOC.Persistence;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddMultiTenancy()
    .WithResolutionStrategy<HostResolutionStrategy>()
    .WithStore<DbContextTenantStore>();

builder.Services.AddDbContext<TenantAdminDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TenantAdmin")));

builder.Services.AddDbContext<SingleTenantDbContext>();


builder.Services.AddTransient<ITenantAccessor<Tenant>, TenantAccessor>();
builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMultiTenancy();
app.UseAuthorization();

app.MapControllers();

app.Run();
