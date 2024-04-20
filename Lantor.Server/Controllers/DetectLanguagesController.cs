using Lantor.DomainModel;
using Lantor.Server.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lantor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectLanguagesController : ControllerBase
    {
        private readonly ILanguageDetectorService languageDetectorService;

        public DetectLanguagesController(ILanguageDetectorService languageDetectorService) 
        {
            this.languageDetectorService = languageDetectorService;
        }

        [HttpPost(Name = "DetectLanguages")]
        public LanguageSimilarityResult Post(LanguageDetectorRequestDTO request)
        {
            if (request.Text == null)
            {
                return new LanguageSimilarityResult();
            }
            else
            {
                return languageDetectorService.Detect(request.Text);
            }
        }
    }
}
