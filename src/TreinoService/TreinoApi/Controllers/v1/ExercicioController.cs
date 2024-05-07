using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TreinoApi.Dtos.Requests;
using TreinoApi.Dtos.Responses;
using TreinoApplication.Abstractions;
using TreinoDomain.Entities;

namespace TreinoApi.Controllers.v1
{
	[Route("/v1/exercicio")]
	public sealed class ExercicioController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IExercicioService _service;

		public ExercicioController(IMapper mapper, IExercicioService service)
		{
			_mapper = mapper;
			_service = service;
		}

		[HttpPost]
		[Route("add")]
		[ProducesResponseType(typeof(ExercicioResponse), 200),
		ProducesResponseType(500)]
		public async Task<IActionResult> Add([FromBody] ExercicioRequest request, CancellationToken ct)
		{
			try
			{
				Exercicio exercicio = _mapper.Map<Exercicio>(request);
				await _service.Add(exercicio, ct);

				return Ok(_mapper.Map<ExercicioResponse>(exercicio));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Ocorreu um erro ao adicionar o exercicio: {ex.Message}");
			}
		}

		[HttpGet]
		[Route("get/{id}")]
		[ProducesResponseType(typeof(ExercicioResponse), 200),
		ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Get(Guid id, CancellationToken ct)
		{
			try
			{
				Exercicio? exercicio = await _service.Get(id, ct);

				if (exercicio is null)
				{
					return NotFound("Exercicio não encontrado");
				}

				return Ok(_mapper.Map<ExercicioResponse>(exercicio));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Ocorreu um erro durante a busca do exercicio: {ex.Message}");
			}
		}

		[HttpGet]
		[Route("get")]
		[ProducesResponseType(typeof(List<ExercicioResponse>), 200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Get(CancellationToken ct)
		{
			try
			{
				List<Exercicio> exercicios = await _service.Get(ct);

				if (exercicios.Count == 0)
				{
					return NotFound("Não foi encontrado nenhum exercicio.");
				}

				return Ok(_mapper.Map<List<ExercicioResponse>>(exercicios));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Ocorreu um erro durante a busca dos exercicios: {ex.Message}");
			}
		}

		[HttpPut]
		[Route("update")]
		[ProducesResponseType(typeof(ExercicioResponse), 200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Update([FromBody] ExercicioRequest request, CancellationToken ct)
		{
			try
			{
				Exercicio exercicio = _mapper.Map<Exercicio>(request);
				Exercicio? result = await _service.Update(exercicio, ct);

				if (result is null)
				{
					return NotFound("Ocorreu um erro ao atualizar o exercicio: Exercicio não encontrado");
				}

				return Ok(_mapper.Map<ExercicioResponse>(result));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					 $"Ocorreu um erro ao atualizar o exercicio: {ex.Message}");
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
					return NotFound("Exercicio não encontrado durante o processo de deletar");
				}

				return Ok("Exercicio deletado");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Ocorreu um erro ao deletar o exercicio: {ex.Message}");
			}
		}
	}
}
