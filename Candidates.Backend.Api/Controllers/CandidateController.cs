﻿using Candidates.Backend.Application.Candidates;
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

        /// <summary>
        /// This Api is to get GetAllCandidatesAsync 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Route("get-all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCandidatesAsync()
        {
            try
            {
                var result = await _candidateService.GetAllCandidatesAsync();
                if (result.Count == 0)
                {
                    return NoContent();
                }
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
                
                if (result == null) return NoContent();

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
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _candidateService.SaveAsync(candidate);

                if (result == null) return NoContent();

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
                
                if (!result) return BadRequest();

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{candidateId:guid}")]
        public async Task<IActionResult> DeleteCandidateAsync(Guid candidateId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

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
