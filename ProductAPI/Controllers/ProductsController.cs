using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Context;
using ProductAPI.Models;


namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ICapPublisher _capPublisher;

        public ProductsController(MyDbContext dbContext, ICapPublisher capPublisher)
        {
            _context = dbContext;
            _capPublisher = capPublisher;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _context.Set<Product>().ToList();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();

            using var transaction = _context.Database.BeginTransaction(_capPublisher, autoCommit: true);

            await _capPublisher.PublishAsync<Product>("product-add", product);
            return Ok(product);
        }

        [CapSubscribe("customer-add")]

        public void GetCustomer(Customer customer)
        {
            Console.WriteLine(customer.Name);
        }

        [HttpPost("addcustomer")]
        public void AddCustomer(Customer customer)
        {
            try
            {
                _context.Add(customer);
                _context.SaveChanges();


                Console.WriteLine("Yeni müşteri eklendi");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
    }
}