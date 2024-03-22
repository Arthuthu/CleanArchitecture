using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos.Requests;
using UserApi.Dtos.Responses;
using UserApplication.Abstractions.AppServices;
using UserDomain.Entities;

namespace UserApi.Controllers.v1
{
    public class UserController : Controller
    {
        private readonly IUserAppService _service;
        private readonly IMapper _mapper;

        public UserController(IUserAppService appService, IMapper mapper)
        {
            _mapper = mapper;
            _service = appService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("v1/user/add")]
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
                return BadRequest($"An error has occurred when registering the user {ex.Message} ");
            }
        }

        [HttpGet]
        [Route("v1/user/get/{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                User? result = await _service.Get(id, cancellationToken);

                if (result is null)
                {
                    return NotFound("User not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occurred while searching for the user {ex.Message}");
            }
        }

        [HttpGet]
        [Route("v1/users/get")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                List<User> result = await _service.Get(cancellationToken);

                if (result.Count == 0)
                {
                    return NotFound("No users found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occurred when searching for the users: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("v1/users/update")]
        public async Task<IActionResult> Update([FromBody] UserRequest userRequest, CancellationToken cancellationToken)
        {
            try
            {
                User user = _mapper.Map<User>(userRequest);
                User? result = await _service.Update(user, cancellationToken);

                if (result is null)
                {
                    return NotFound("User not found");
                }

                return Ok("User updated");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has ocurred when updating the user: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("v1/user/delete/{id}")]
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
                return BadRequest($"An error has ocurred while deleting the user: {ex.Message}");
            }
        }
    }
}
