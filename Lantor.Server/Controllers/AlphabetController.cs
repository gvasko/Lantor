using AutoMapper;
using Lantor.DomainModel;
using Lantor.Server.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lantor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlphabetController : ControllerBase
    {
        private readonly ISampleRepository sampleRepository;
        private readonly IMapper mapper;

        public AlphabetController(ISampleRepository sampleRepository, IMapper mapper)
        {
            this.sampleRepository = sampleRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlphabetListInfoDTO>>> GetAll()
        {
            var all = await sampleRepository.GetAllAlphabetsAsync();
            var result = mapper.Map<List<AlphabetListInfoDTO>>(all);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AlphabetListInfoDTO>> Create(AlphabetListInfoDTO newDTO)
        {
            if (string.IsNullOrEmpty(newDTO.Name))
            {
                return BadRequest("Alphabet name cannot be null or empty");
            }
            var newEntity = await sampleRepository.CreateAlphabetAsync(newDTO.Name, newDTO.Dim);
            var newEntityDTO = mapper.Map<AlphabetListInfoDTO>(newEntity);
            return Ok(newEntityDTO);
        }

    }
}
