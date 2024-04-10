using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos.Requests;
using UserApi.Dtos.Responses;
using UserApplication.Abstractions.AppServices;
using UserDomain.Entities;

namespace UserApi.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class UserController : Controller
    {
        private readonly IUserAppService _service;
        private readonly IMapper _mapper;

        public UserController(IUserAppService appService, IMapper mapper)
        {
            _mapper = mapper;
            _service = appService;
        }

        [HttpPost]
        [Route("v1/user/add")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK),
         ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(500)]
        public async Task<IActionResult> Add([FromBody] UserRequest userRequest, CancellationToken cancellationToken)
        {
            try
            {
                User user = _mapper.Map<User>(userRequest);
                User? result = await _service.Add(user, cancellationToken);

                if (user is null)
                {
                    return BadRequest("This email has already been registered");
                }

                return Ok(_mapper.Map<UserResponse>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has occurred when registering the user {ex.Message} ");
            }
        }

        [HttpGet]
        [Route("v1/user/get/{id}")]
        [ProducesResponseType(typeof(UserResponse), 200),
         ProducesResponseType(404), ProducesResponseType(500)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                User? user = await _service.Get(id, cancellationToken);

                if (user is null)
                {
                    return NotFound("User not found");
                }

                return Ok(_mapper.Map<UserResponse>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has occurred while searching for the user {ex.Message}");
            }
        }

        [HttpGet]
        [Route("v1/user/get")]
		[ProducesResponseType(typeof(List<UserResponse>), 200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                List<User> users = await _service.Get(cancellationToken);

                if (users.Count == 0)
                {
                    return NotFound("No users found");
                }

                return Ok(_mapper.Map<List<UserResponse>>(users));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has occurred when searching for the users: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("v1/user/{userId}/update")]
		[ProducesResponseType (200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Update(Guid userId, [FromBody] UserRequest userRequest, CancellationToken cancellationToken)
        {
            try
            {
                User user = _mapper.Map<User>(userRequest);
                User? result = await _service.Update(userId, user, cancellationToken);

                if (result is null)
                {
                    return NotFound("User not found");
                }

                return Ok(_mapper.Map<UserResponse>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has ocurred when updating the user: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("v1/user/delete/{id}")]
		[ProducesResponseType(200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                bool result = await _service.Delete(id, cancellationToken);

                if (result is false)
                {
                    return NotFound("User not found");
                }

                return Ok("User successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has ocurred while deleting the user: {ex.Message}");
            }
        }
    }
}
