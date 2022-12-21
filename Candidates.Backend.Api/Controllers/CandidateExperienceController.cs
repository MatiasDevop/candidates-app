using AutoMapper;
using Candidates.Backend.Application.Dtos;
using Candidates.Backend.Application.Experiences;
using Candidates.Backend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Candidates.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateExperienceController : ControllerBase
    {
        private readonly IExperienceService _experienceService;
        private readonly IMapper _mapper;
        public CandidateExperienceController(IExperienceService experienceService, IMapper mapper)
        {
            _experienceService = experienceService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExperienceAsync([FromBody] ExperienceDto experience)
        {
            try
            {
                var newExperience = _mapper.Map<CandidateExperience>(experience);
                var result = await _experienceService.SaveCandidateExperiencieAsync(newExperience);

                return Ok(result.CandidateExperienceId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
