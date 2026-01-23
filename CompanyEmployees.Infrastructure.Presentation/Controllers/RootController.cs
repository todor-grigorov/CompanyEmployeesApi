using CompanyEmployees.Core.Domain.Entities;
using CompanyEmployees.Core.Domain.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CompanyEmployees.Infrastructure.Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        public RootController(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType.Contains("application/tg.code.apiroot"))
            {
                var list = new List<Link>
        {
            new Link
            {
                Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new {}),
                Rel = "self",
                Method = "GET"
            },
            new Link
            {
                Href = _linkGenerator.GetUriByAction(HttpContext, "GetCompanies", "companies"),
                Rel = "companies",
                Method = "GET"
            },
            new Link
            {
                Href = _linkGenerator.GetUriByAction(HttpContext, "CreateCompany", "companies", new Company()),
                Rel = "create_company",
                Method = "POST"
            }
        };

                return Ok(list);
            }

            return NoContent();
        }
    }
}
