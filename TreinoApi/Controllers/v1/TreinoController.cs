using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TreinoApi.Dtos.Requests;
using TreinoApi.Dtos.Responses;
using TreinoApplication.Abstractions;
using TreinoDomain.Entities;

namespace TreinoApi.Controllers.v1
{
	[Route("/v1/treino")]
	public class TreinoController : Controller
	{
		private readonly IMapper _mapper;
		private readonly ITreinoService _service;

		public TreinoController(IMapper mapper, ITreinoService service)
		{
			_mapper = mapper;
			_service = service;
		}

		[HttpPost]
		[Route("add")]
		[ProducesResponseType(typeof(TreinoResponse), 200),
		ProducesResponseType(500)]
		public async Task<IActionResult> Add([FromBody] TreinoRequest request, CancellationToken ct)
		{
			try
			{
				Treino treino = _mapper.Map<Treino>(request);
				await _service.Add(treino, ct);

				return Ok(_mapper.Map<TreinoResponse>(treino));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Ocorreu um erro ao adicionar o treino: {ex.Message}");
			}
		}

		[HttpGet]
		[Route("get/{id}")]
		[ProducesResponseType(typeof(TreinoResponse), 200),
		ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Get(Guid id, CancellationToken ct)
		{
			try
			{
				Treino? treino = await _service.Get(id, ct);

				if (treino is null)
				{
					return NotFound("Treino não encontrado");
				}

				return Ok(_mapper.Map<TreinoResponse>(treino));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Ocorreu um erro durante a busca do treino: {ex.Message}");
			}
		}

		[HttpGet]
		[Route("get")]
		[ProducesResponseType(typeof(List<TreinoResponse>), 200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Get(CancellationToken ct)
		{
			try
			{
				List<Treino> treinos = await _service.Get(ct);

				if (treinos.Count == 0)
				{
					return NotFound("Não foi encontrado nenhum treino.");
				}

				return Ok(_mapper.Map<List<TreinoResponse>>(treinos));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Ocorreu um erro durante a busca dos treinos: {ex.Message}");
			}
		}

		[HttpPut]
		[Route("update")]
		[ProducesResponseType(typeof(TreinoResponse), 200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Update([FromBody] TreinoRequest request, CancellationToken ct)
		{
			try
			{
				Treino treino = _mapper.Map<Treino>(request);
				Treino? result = await _service.Update(treino, ct);

				if (result is null)
				{
					return NotFound("Ocorreu um erro ao atualizar o treino: Treino não encontrado");
				}

				return Ok(_mapper.Map<TreinoResponse>(result));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					 $"Ocorreu um erro ao atualizar o treino: {ex.Message}");
			}
		}

		[HttpDelete]
		[Route("delete/{id}")]
		[ProducesResponseType(200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
		{
			try
			{
				bool result = await _service.Delete(id, ct);

				if (result is false)
				{
					return NotFound("Treino não encontrado durante o processo de deletar");
				}

				return Ok("Treino deletado");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Ocorreu um erro ao deletar o treino: {ex.Message}");
			}
		}
	}
}
