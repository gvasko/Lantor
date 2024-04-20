﻿using AutoMapper;
using Lantor.Data.Infrastructure;
using Lantor.DomainModel;
using Lantor.Server.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lantor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageSampleController : ControllerBase
    {
        private readonly ISampleRepository sampleRepository;
        private readonly IMapper mapper;

        public LanguageSampleController(ISampleRepository sampleRepository, IMapper mapper)
        {
            this.sampleRepository = sampleRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LanguageSampleDTO>> GetById(int id)
        {
            var mls = await sampleRepository.GetLanguageSampleAsync(id);
            var result = mapper.Map<LanguageSampleDTO>(mls);
            return Ok(result);
        }
    }
}