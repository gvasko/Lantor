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

        // TODO: LanguageSimilarityResultDTO?   
        [HttpPost(Name = "DetectLanguages")]
        public async Task<LanguageSimilarityResult> Post(LanguageDetectorRequestDTO request)
        {
            if (request.Text == null)
            {
                return new LanguageSimilarityResult();
            }
            else
            {
                if (request.SampleId == 0 || request.AlphabetId == 0)
                {
                    return await languageDetectorService.Detect(request.Text);
                } else
                {
                    return await languageDetectorService.Detect(request.SampleId, request.AlphabetId, request.Text);
                }
            }
        }
    }
}
