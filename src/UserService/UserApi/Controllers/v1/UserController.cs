using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("v1/user/get/{id}")]
        [ProducesResponseType(typeof(UserResponse), 200),
         ProducesResponseType(404), ProducesResponseType(500)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
				User? user = await _service.Get(id);

                if (user is null)
                {
                    return NotFound(StatusCodes.Status404NotFound);
                }

                return Ok(_mapper.Map<UserResponse>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has occurred while searching for the user {ex.Message}");
            }
        }

        [HttpGet, AllowAnonymous]
        [Route("v1/user/get")]
		[ProducesResponseType(typeof(List<UserResponse>), 200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Get()
        {
            try
            {
                List<User> users = await _service.Get();

                if (users.Count == 0)
                {
                    return NotFound(StatusCodes.Status404NotFound);
                }

                return Ok(_mapper.Map<List<UserResponse>>(users));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has occurred when searching for the users: {ex.Message}");
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
                    return NotFound(StatusCodes.Status404NotFound);
                }

                return Ok("Usuário deletado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Ocorreu um erro ao deletar o usuário: {ex.Message}");
            }
        }

		[HttpGet]
		[Route("v1/user/getbyemail/{email}")]
		[ProducesResponseType(typeof(UserResponse), 200),
        ProducesResponseType(404), ProducesResponseType(500)]
        [AllowAnonymous]
		public async Task<IActionResult> GetByEmail(string email)
		{
			try
			{
				User? user = await _service.GetByEmail(email);

				if (user is null)
				{
					return NotFound(StatusCodes.Status404NotFound);
				}

				return Ok(_mapper.Map<UserResponse>(user));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Ocorreu um erro ao encontrar o usuário por email: {ex.Message}");
			}
		}

        [HttpGet]
        [Route("v1/user/getbyusername/{username}")]
        [ProducesResponseType(typeof(UserResponse), 200),
        ProducesResponseType(404), ProducesResponseType(500)]
        [AllowAnonymous]
        public async Task <IActionResult> GetByUsername(string username)
        {
            try
            {
				User? user = await _service.GetByUsername(username);

                if(user is null)
                {
                    return NotFound(StatusCodes.Status404NotFound);
                }

                return Ok(_mapper.Map<UserResponse>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ocorreu um erro ao encontrar o usuário por username: { ex.Message }");
                throw;
            }
        }
	}
}
