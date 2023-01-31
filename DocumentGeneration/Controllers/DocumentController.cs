using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGeneration.Controllers
{
    [Route("api/[controller]")]
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
            _generate.GeneratePdf(documentGenerateModels);

            return Ok(documentGenerateModels);
        }

    }
}
