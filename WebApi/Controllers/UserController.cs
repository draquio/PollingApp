using Application.Commands.Users.Create;
using Application.Commands.Users.Delete;
using Application.Commands.Users.Update;
using Application.DTOs.User;
using Application.Queries.Users.GetAll;
using Application.Queries.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ResponsePaged<List<UserReadDTO>>>> GetUsers(int page = 1, int pageSize = 10)
        {
            var rsp = new ResponsePaged<List<UserReadDTO>>();
            try
            {
                var query = new GetAllUsersQuery(page, pageSize);
                rsp.page = page;
                rsp.pageSize = pageSize;
                rsp.value = await _mediator.Send(query);
                rsp.status = true;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(500, rsp);
            }
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<Response<UserReadDTO>>> GetUser(int userId)
        {
            var rsp = new Response<UserReadDTO>();
            try
            {
                var query = new GetByIdUserQuery(userId);
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
        public async Task<ActionResult<Response<UserReadDTO>>> CreateUser([FromBody] UserCreateDTO user)
        {
            var rsp = new Response<UserReadDTO>();
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
                var command = new CreateUserCommand(user);
                rsp.status = true;
                rsp.value = await _mediator.Send(command);
                rsp.msg = "User created successfully";
                return CreatedAtAction(nameof(GetUser), new { userId = rsp.value.Id }, rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return StatusCode(500, rsp);
            }
        }
        [HttpPut("{userId:int}")]
        public async Task<ActionResult<Response<bool>>> UpdateUser([FromBody] UserUpdateDTO user, int userId)
        {
            var rsp = new Response<bool>();
            try
            {
                if (user.Id != userId)
                {
                    rsp.status = false;
                    rsp.msg = "User ID mismatch";
                    return BadRequest(rsp);
                }
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
                var command = new UpdateUserCommand(user);
                rsp.status = true;
                rsp.msg = "User updated successfully";
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

        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<Response<bool>>> DeleteUser(int userId)
        {
            var rsp = new Response<bool>();
            try
            {
                var command = new DeleteUserCommand(userId);
                rsp.status = true;
                rsp.msg = "User deleted successfully";
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
