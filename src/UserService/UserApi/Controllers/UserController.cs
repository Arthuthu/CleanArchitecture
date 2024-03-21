using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos.Requests;
using UserApi.Dtos.Responses;
using UserApplication.Abstractions.AppServices;
using UserDomain.Entities;

namespace UserApi.Controllers
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
        public async Task<IActionResult> Add([FromBody]UserRequest userRequest, CancellationToken cancellationToken)
		{
			try
			{
				User user = _mapper.Map<User>(userRequest);
				User? result = await _service.Add(user, cancellationToken);

				if (user is null)
				{
					return BadRequest("Esse email ja esta sendo utilizado");
				}

				return Ok(_mapper.Map<UserResponse>(result));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
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
					return NotFound("Usuario não encontrado");
				}

				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
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
					return NotFound("Nenhum usuário encontrado no banco de dados");
				}

				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest($"Ocorreu um erro ao pesquisar os usuários: { ex.Message }");
			}
		}

		[HttpPut]
		[Route("v1/users/update")]
		public async Task<IActionResult> Update(UserRequest userRequest, CancellationToken cancellationToken)
		{
			try
			{
				User user = _mapper.Map<User>(userRequest);
				await _service.Update(user, cancellationToken);

				return Ok("Usuario atualizado");
			}
			catch (Exception ex)
			{
				return BadRequest($"Ocorreu um erro ao atualizar o usuário { ex.Message }");
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
					return NotFound("Usuário não encontrado");
				}

				return Ok("Usuário deletado com sucesso");
			}
			catch (Exception ex)
			{
				return BadRequest($"Ocorreu um erro interno ao deletar o usuário { ex.Message }");
			}
		}
	}
}
