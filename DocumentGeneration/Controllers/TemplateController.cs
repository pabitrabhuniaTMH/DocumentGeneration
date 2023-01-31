using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGeneration.Controllers
{
    [Route("api/[controller]")]
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
            
            return Ok(_template.SaveTemplate(template));
        }
    }
}
