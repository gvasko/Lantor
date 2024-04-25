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
        public ActionResult<List<AlphabetListInfoDTO>> GetAll()
        {
            var all = sampleRepository.GetAllAlphabets();
            var result = mapper.Map<List<AlphabetListInfoDTO>>(all);
            return Ok(result);
        }

    }
}
