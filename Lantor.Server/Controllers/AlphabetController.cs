using AutoMapper;
using Lantor.DomainModel;
using Lantor.Server.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Lantor.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AlphabetController : ControllerBase
    {
        private readonly IDomainUnitOfWork domainUow;
        private readonly IMapper mapper;

        public AlphabetController(IDomainUnitOfWork domainUow, IMapper mapper)
        {
            this.domainUow = domainUow;
            this.mapper = mapper;
        }

        [HttpGet]
        //[RequiredScopeOrAppPermission(
        //    RequiredScopesConfigurationKey = "AzureAD:Scopes:AdvancedUsage"
        //)]
        public async Task<ActionResult<List<AlphabetListInfoDTO>>> GetAll()
        {
            var all = await domainUow.BasicCrudOperations.GetAllAlphabetListInfoAsync();
            var result = mapper.Map<List<AlphabetListInfoDTO>>(all);
            return Ok(result);
        }

        [HttpPost]
        //[RequiredScopeOrAppPermission(
        //    RequiredScopesConfigurationKey = "AzureAD:Scopes:AdvancedUsage"
        //)]
        public async Task<ActionResult<AlphabetListInfoDTO>> Create(AlphabetListInfoDTO newDTO)
        {
            if (string.IsNullOrEmpty(newDTO.Name))
            {
                return BadRequest("Alphabet name cannot be null or empty");
            }
            var newEntity = await domainUow.CreateAlphabetAsync(newDTO.Name, newDTO.Dim);
            await domainUow.Save();
            var newEntityDTO = mapper.Map<AlphabetListInfoDTO>(newEntity);
            return Ok(newEntityDTO);
        }

        [HttpDelete("{id:int}")]
        //[RequiredScopeOrAppPermission(
        //    RequiredScopesConfigurationKey = "AzureAD:Scopes:AdvancedUsage"
        //)]
        public async Task<ActionResult> Delete(int id)
        {
            await domainUow.RemoveAlphabetAsync(id);
            await domainUow.Save();
            return Ok();
        }
    }
}
