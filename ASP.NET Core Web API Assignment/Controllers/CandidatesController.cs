using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Core.Dtos;
using Core.Entities;

namespace Sigma_Software_Task.Controllers
{
    public class CandidatesController : ControllerBase
    {
        private ICandidateService _candidateService;
        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet("/{emailAddress}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Get(string emailAddress)
        {
            return Ok(await _candidateService.GetCandidateByEmailAddress(emailAddress));
        }


        [HttpPost("/")]
        public async Task<IActionResult> Post([FromBody]Candidate candidate, [FromQuery] bool saveOnServer=false)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _candidateService.InsertOrUpdateCandidate(candidate, saveOnServer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}