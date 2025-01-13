using Application.Commands.Polls.Create;
using Application.Commands.Polls.Delete;
using Application.DTOs.Poll;
using Application.Queries.Polls.GetAll;
using Application.Queries.Polls.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PollController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ResponsePaged<List<PollReadDTO>>>> GetPolls(int page = 1, int pageSize = 10)
        {
            var rsp = new ResponsePaged<List<PollReadDTO>>();
            try
            {
                var query = new GetAllPollsQuery(page, pageSize);
                rsp.status = true;
                rsp.page = page;
                rsp.pageSize = pageSize;
                rsp.value = await _mediator.Send(query);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(500, rsp);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Response<PollReadDTO>>> GetPollById(int id)
        {
            var rsp = new Response<PollReadDTO>();
            try
            {
                var query = new GetByIdPollQuery(id);
                rsp.status = true;
                rsp.value = await _mediator.Send(query);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(500, rsp);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<PollReadDTO>>> CreatePoll([FromBody] PollCreateDTO poll)
        {
            var rsp = new Response<PollReadDTO>();
            try
            {
                var command = new CreatePollCommand(poll);
                rsp.status = true;
                rsp.value = await _mediator.Send(command);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(500, rsp);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response<bool>>> DeletePoll(int id)
        {
            var rsp = new Response<bool>();
            try
            {
                var command = new DeletePollCommand(id);
                rsp.status = true;
                rsp.msg = "Poll deleted successfully";
                rsp.value = await _mediator.Send(command);
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
