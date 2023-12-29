using CustomerAPI.Context;
using CustomerAPI.Interface;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly MyDbContext _context;
        protected readonly IServiceCallHelper _serviceCallHelper;
        protected readonly ICapPublisher _capPublisher;
        protected readonly ICapHelper _capHelper;

        public BaseController(IConfiguration configuration, MyDbContext context, IServiceCallHelper callHelper, ICapPublisher capPublisher, ICapHelper capHelper)
        {
            _configuration = configuration;
            _context = context;
            _serviceCallHelper = callHelper;
            _capPublisher = capPublisher;
            _capHelper = capHelper;
        }
    }
}
