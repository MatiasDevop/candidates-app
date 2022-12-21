using Candidates.Backend.Application.Candidates;
using Candidates.Backend.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Candidates.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllCandidatesAsync()
        {
            try
            {
                var result = await _candidateService.GetAllCandidatesAsync();

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{candidateId}")]
        public async Task<IActionResult> GetCandidateByIdAsync(Guid candidateId)
        {
            try
            {
                var result = await _candidateService.GetByIdAsync(candidateId);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidateAsync([FromBody] CandidateDto candidate)
        {
            try
            {
                var result = await _candidateService.SaveAsync(candidate);

                return Ok(result.CandidateId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("{candidateId}")]
        public async Task<IActionResult> UpdateCandidateAsync(Guid candidateId, [FromBody] CandidateDto candidate)
        {
            try
            {
                var result = await _candidateService.UpdateCandidateAsync(candidateId, candidate);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{candidateId}")]
        public async Task<IActionResult> DeleteCandidateAsync(Guid candidateId)
        {
            try
            {
                await _candidateService.DeleteCandidateAsync(candidateId);

                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
