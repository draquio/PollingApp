using Application.DTOs.Vote;
using MediatR;
using WebApi.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Votes.AddVote;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Response<bool>>> CreateVote([FromBody] VoteCreateDTO vote)
        {
            var rsp = new Response<bool>();
            try
            {
                if (!ModelState.IsValid)
                {
                    rsp.status = false;
                    rsp.msg = "Invalid data";
                    rsp.errors = ModelState.Values
                        .SelectMany(err => err.Errors)
                        .Select(err => err.ErrorMessage)
                        .ToList();
                    return BadRequest(rsp);
                }

                string? userIp = HttpContext.Connection.RemoteIpAddress?.ToString();
                var command = new AddVoteCommand(vote, userIp);
                rsp.status = true;
                rsp.value = await _mediator.Send(command);
                rsp.msg = "You voted successfully";
                return Ok(rsp);

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(500, rsp);
            }

        }
    }
}
