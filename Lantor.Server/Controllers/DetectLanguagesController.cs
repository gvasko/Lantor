using Lantor.Server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lantor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectLanguagesController : ControllerBase
    {
        [HttpPost(Name = "DetectLanguages")]
        public LanguageSimilarityResult Post(LanguageDetectorRequest request)
        {
            var result = new LanguageSimilarityResult(
                new LanguageSimilarityValue("en", 0.15),
                new LanguageSimilarityValue("de", 0.11),
                new LanguageSimilarityValue("hu", 0.05));
            return result;
        }
    }
}
