using Lantor.DomainModel;
using Lantor.Server.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Lantor.Server.Controllers
{
    [Authorize]
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
        [HttpPost(Name = "DefaultDetectLanguages")]
        [RequiredScopeOrAppPermission(
            RequiredScopesConfigurationKey = "AzureAD:Scopes:BasicUsage",
            RequiredAppPermissionsConfigurationKey = "AzureAD:AppPermissions:BasicUsage"
        )]
        public async Task<LanguageSimilarityResult> DefaultPost(string request)
        {
            return await languageDetectorService.Detect(request);
        }

        // TODO: LanguageSimilarityResultDTO?   
        [HttpPost(Name = "CustomDetectLanguages")]
        [RequiredScopeOrAppPermission(
            RequiredScopesConfigurationKey = "AzureAD:Scopes:AdvancedUsage"
        )]
        public async Task<LanguageSimilarityResult> CustomPost(LanguageDetectorRequestDTO request)
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
                }
                else
                {
                    return await languageDetectorService.Detect(request.SampleId, request.AlphabetId, request.Text);
                }
            }
        }
    }
}
