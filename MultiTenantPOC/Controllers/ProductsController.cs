using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantPOC.Models;
using MultiTenantPOC.Persistence;

namespace MultiTenantPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SingleTenantDbContext _context;

        public ProductsController(SingleTenantDbContext context)
        {
            _context = context;
        }
        public ICollection<Product> Products { get; set; }

        [HttpGet]
        public ActionResult<ICollection<Product>> getProducts()
        {
            return _context.Products.ToList();
        }
    }
}
