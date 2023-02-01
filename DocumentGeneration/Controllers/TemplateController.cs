using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGeneration.Controllers
{
    [Route("api/v0.0.1/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplate _template;
        public TemplateController(ITemplate template)
        {
            _template=template;
        }
        [HttpPost("SaveTemplate")]
        public IActionResult SaveTemplate(TemplateModel template)
        {          
            var Results= Ok(_template.SaveTemplate(template));
            return Ok(Results);
        }
    }
}
