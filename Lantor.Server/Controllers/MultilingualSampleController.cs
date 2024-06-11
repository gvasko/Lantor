using AutoMapper;
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
    public class MultilingualSampleController : ControllerBase
    {
        private readonly IDomainUnitOfWork domainUow;
        private readonly IMapper mapper;

        public MultilingualSampleController(IDomainUnitOfWork domainUow, IMapper mapper)
        {
            this.domainUow = domainUow;
            this.mapper = mapper;
        }

        [HttpGet]
        [RequiredScope(AuthScopes.SAMPLES_MANAGE_BY_OWNER)]
        public async Task<ActionResult<List<MultilingualSampleListInfoDTO>>> GetAll()
        {
            var all = await domainUow.BasicCrudOperations.GetAllMultilingualSamplesAsync();
            var result = mapper.Map<List<MultilingualSampleListInfoDTO>>(all);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [RequiredScope(AuthScopes.SAMPLES_MANAGE_BY_OWNER)]
        public async Task<ActionResult<EmptyMultilingualSampleDTO>> GetById(int id)
        {
            var mls = await domainUow.BasicCrudOperations.GetMultilingualSampleAsync(id);
            var result = mapper.Map<EmptyMultilingualSampleDTO>(mls);
            return Ok(result);
        }

        [HttpPut]
        [RequiredScope(AuthScopes.SAMPLES_MANAGE_BY_OWNER)]
        public async Task<IActionResult> Update(EmptyMultilingualSampleDTO updatedDTO)
        {
            if (!ModificationAllowedForMultilingualSample(updatedDTO.Id))
            {
                return Forbid();
            }

            var updatedDM = mapper.Map<MultilingualSample>(updatedDTO);
            domainUow.BasicCrudOperations.UpdateMultilingualSample(updatedDM);
            await domainUow.Save();
            return Ok();
        }

        [HttpPost]
        [RequiredScope(AuthScopes.SAMPLES_MANAGE_BY_OWNER)]
        public async Task<ActionResult<EmptyMultilingualSampleDTO>> Create(EmptyMultilingualSampleDTO newDTO)
        {
            var newDM = mapper.Map<MultilingualSample>(newDTO);
            var newEntity = await domainUow.BasicCrudOperations.CreateMultilingualSampleAsync(newDM);
            await domainUow.Save();
            var newEntityDTO = mapper.Map<EmptyMultilingualSampleDTO>(newEntity);
            return Ok(newEntityDTO);
        }

        [HttpDelete("{id:int}")]
        [RequiredScope(AuthScopes.SAMPLES_MANAGE_BY_OWNER)]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModificationAllowedForMultilingualSample(id))
            {
                return Forbid();
            }

            await domainUow.RemoveMultilingualSampleAsync(id);
            await domainUow.Save();
            return Ok();
        }

        private bool ModificationAllowedForMultilingualSample(int id)
        {
            return id > 1;
        }
    }
}
