﻿using AutoMapper;
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
        private readonly ISampleRepository sampleRepository;
        private readonly IMapper mapper;

        public MultilingualSampleController(ISampleRepository sampleRepository, IMapper mapper)
        {
            this.sampleRepository = sampleRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MultilingualSampleListInfoDTO>>> GetAll()
        {
            var all = await sampleRepository.GetAllMultilingualSamplesAsync();
            var result = mapper.Map<List<MultilingualSampleListInfoDTO>>(all);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmptyMultilingualSampleDTO>> GetById(int id)
        {
            var mls = await sampleRepository.GetMultilingualSampleAsync(id);
            var result = mapper.Map<EmptyMultilingualSampleDTO>(mls);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(EmptyMultilingualSampleDTO updatedDTO)
        {
            var updatedDM = mapper.Map<MultilingualSample>(updatedDTO);
            await sampleRepository.UpdateMultilingualSample(updatedDM);
            return Ok();
        }
    }
}
