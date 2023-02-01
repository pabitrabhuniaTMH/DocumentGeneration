using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGeneration.Controllers
{
    [Route("api/v0.0.1/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IGenerate _generate;
        public DocumentController(IGenerate generate)
        {
            _generate = generate;
        }
        [HttpPost("GenerateDocument")]
        public IActionResult GenerateDocument(DocumentGenerateModels documentGenerateModels)
        {
           var result= _generate.GeneratePdf(documentGenerateModels);

            return Ok(result);
        }

    }
}
