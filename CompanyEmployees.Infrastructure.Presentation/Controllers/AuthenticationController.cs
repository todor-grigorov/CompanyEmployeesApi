using CompanyEmployees.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Infrastructure.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController()]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service) => _service = service;
    }
}
