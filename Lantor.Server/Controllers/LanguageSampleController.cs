using AutoMapper;
using Lantor.Data.Infrastructure;
using Lantor.DomainModel;
using Lantor.Server.DTO;
using Lantor.Server.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Lantor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageSampleController : ControllerBase
    {
        private readonly IDomainUnitOfWork domainUow;
        private readonly IMapper mapper;

        public LanguageSampleController(IDomainUnitOfWork domainUow, IMapper mapper)
        {
            this.domainUow = domainUow;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [RequiredScope(AuthScopes.SAMPLES_MANAGE_BY_OWNER)]
        public async Task<ActionResult<LanguageSampleDTO>> GetById(int id)
        {
            var mls = await domainUow.BasicCrudOperations.GetLanguageSampleAsync(id);
            var result = mapper.Map<LanguageSampleDTO>(mls);
            return Ok(result);
        }

        [HttpPut]
        [RequiredScope(AuthScopes.SAMPLES_MANAGE_BY_OWNER)]
        public async Task<IActionResult> Update(LanguageSampleDTO updatedDTO)
        {
            if (!await ModificationAllowedForLanguageSample(updatedDTO.Id))
            {
                return Forbid();
            }

            var updatedDM = mapper.Map<LanguageSample>(updatedDTO);
            domainUow.UpdateLanguageSample(updatedDM);
            await domainUow.Save();
            return Ok();
        }

        [HttpPost]
        [RequiredScope(AuthScopes.SAMPLES_MANAGE_BY_OWNER)]
        public async Task<ActionResult<LanguageSampleDTO>> Create(LanguageSampleDTO newDTO)
        {
            var newDM = mapper.Map<LanguageSample>(newDTO);
            var newEntity = await domainUow.BasicCrudOperations.CreateLanguageSampleAsync(newDM);
            await domainUow.Save();
            var newEntityDTO = mapper.Map<LanguageSampleDTO>(newEntity);
            return Ok(newEntityDTO);
        }

        [HttpDelete("{id:int}")]
        [RequiredScope(AuthScopes.SAMPLES_MANAGE_BY_OWNER)]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await ModificationAllowedForLanguageSample(id))
            {
                return Forbid();
            }

            await domainUow.RemoveLanguageSampleAsync(id);
            await domainUow.Save();
            return Ok();
        }

        private async Task<bool> ModificationAllowedForLanguageSample(int id)
        {
            var sample = await domainUow.BasicCrudOperations.GetLanguageSampleAsync(id);

            if (sample == null)
            {
                return false;
            }

            return sample.MultilingualSampleId > 1;
        }

    }
}
