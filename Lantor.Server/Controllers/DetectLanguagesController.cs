using Lantor.DomainModel;
using Lantor.Server.DTO;
using Lantor.Server.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Lantor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DetectLanguagesController : ControllerBase
    {
        private readonly IDomainUnitOfWork domainUow;
        private readonly ILanguageDetectorService languageDetectorService;

        public DetectLanguagesController(IDomainUnitOfWork domainUow, ILanguageDetectorService languageDetectorService) 
        {
            this.domainUow = domainUow;
            this.languageDetectorService = languageDetectorService;
        }

        // TODO: LanguageSimilarityResultDTO?   
        [HttpPost, Route("Default")]
        [AllowAnonymous]
        public async Task<LanguageSimilarityResult> DefaultPost(LanguageDetectorRequestDTO request)
        {
            if (request.Text == null)
            {
                return new LanguageSimilarityResult();
            }
            else
            {
                return await languageDetectorService.Detect(request.Text);
            }
        }

        // TODO: LanguageSimilarityResultDTO?   
        [HttpPost, Route("Custom")]
        [RequiredScope(AuthScopes.CUSTOM_DETECTION)]
        public async Task<LanguageSimilarityResult> CustomPost(LanguageDetectorRequestDTO request)
        {
            await domainUow.EnsureCurrentUserFromDataAsync(this.GetUserData());
            if (request.Text == null)
            {
                return new LanguageSimilarityResult();
            }
            else
            {
                if (request.SampleId == 0 || request.AlphabetId == 0)
                {
                    return await languageDetectorService.Detect(request.Text);
                }
                else
                {
                    return await languageDetectorService.Detect(request.SampleId, request.AlphabetId, request.Text);
                }
            }
        }
    }
}
