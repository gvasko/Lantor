using AutoMapper;
using Lantor.DomainModel;
using Lantor.Server.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lantor.Server.Controllers
{
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
        public async Task<ActionResult<List<MultilingualSampleListInfoDTO>>> GetAll()
        {
            var all = await domainUow.BasicCrudOperations.GetAllMultilingualSamplesAsync();
            var result = mapper.Map<List<MultilingualSampleListInfoDTO>>(all);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmptyMultilingualSampleDTO>> GetById(int id)
        {
            var mls = await domainUow.BasicCrudOperations.GetMultilingualSampleAsync(id);
            var result = mapper.Map<EmptyMultilingualSampleDTO>(mls);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmptyMultilingualSampleDTO updatedDTO)
        {
            var updatedDM = mapper.Map<MultilingualSample>(updatedDTO);
            domainUow.BasicCrudOperations.UpdateMultilingualSample(updatedDM);
            await domainUow.Save();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<EmptyMultilingualSampleDTO>> Create(EmptyMultilingualSampleDTO newDTO)
        {
            var newDM = mapper.Map<MultilingualSample>(newDTO);
            var newEntity = await domainUow.BasicCrudOperations.CreateMultilingualSampleAsync(newDM);
            await domainUow.Save();
            var newEntityDTO = mapper.Map<EmptyMultilingualSampleDTO>(newEntity);
            return Ok(newEntityDTO);
        }
    }
}
