using Hogwarts.API.Command;
using Hogwarts.API.Models;
using Hogwarts.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hogwarts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllCandidatesQuery();
            var result = await _mediator.Send(query);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost("SendCandidate")]
        public async Task<IActionResult> Post(CandidateDto candidateDto)
        {
            var query = new CreateCandidateCommand(candidateDto);
            var result = await _mediator.Send(query);

            return CreatedAtAction(nameof(Post), result);
        }

        [HttpPut("UpdateCandidate/{candidateId}")]
        public async Task<IActionResult> Put(int candidateId, [FromBody] CandidateDto candidateDto)
        {
            var query = new UpdateCandidateCommand(candidateId, candidateDto);
            var result = await _mediator.Send(query);

            return CreatedAtAction(nameof(Put), result);
        }

        [HttpDelete("{candidateId}")]
        public async Task<IActionResult> Delete(int candidateId)
        {
            var query = new DeleteCandidateCommand(candidateId);
            var result = await _mediator.Send(query);

            return result ? Ok() : NotFound();
        }
    }
}
