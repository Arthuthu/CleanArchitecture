using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SubscriptionApi.Dtos.Requests;
using SubscriptionApi.Dtos.Responses;
using SubscriptionApplication.Abstractions.AppServices;
using SubscriptionDomain.Entities;

namespace SubscriptionApi.Controllers.v1
{
	public class ContractController : Controller
	{
		private readonly IContractAppService _appService;
		private readonly IMapper _mapper;

		public ContractController(IContractAppService appService, IMapper mapper)
		{
			_appService = appService;
			_mapper = mapper;
		}

		[HttpPost]
		[Route("v1/contract/add")]
		public async Task<IActionResult> Add([FromBody] ContractRequest request, CancellationToken cancellationToken)
		{
			try
			{
				Contract contract = _mapper.Map<Contract>(request);
				await _appService.Add(contract, cancellationToken);

				return Ok(_mapper.Map<ContractResponse>(contract));
			}
			catch (Exception ex)
			{
				return BadRequest($"An error ocurred while adding the contract {ex.Message}");
			}
		}

		[HttpGet]
		[Route("v1/contract/get/{id}")]
		public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				Contract? contract = await _appService.Get(id, cancellationToken);

				if (contract is null)
				{
					return NotFound("Contract not found");
				}

				return Ok(_mapper.Map<ContractResponse>(contract));
			}
			catch (Exception ex)
			{
				return BadRequest($"An error has occurred while searching for the contract: {ex.Message}");
			}
		}

		[HttpGet]
		[Route("v1/contract/get")]
		public async Task<IActionResult> Get(CancellationToken cancellationToken)
		{
			try
			{
				List<Contract> contracts = await _appService.Get(cancellationToken);

				if (contracts.Count == 0)
				{
					return NotFound("No contracts were found");
				}

				return Ok(contracts);

			}
			catch (Exception ex)
			{
				return BadRequest($"An error has occurred while searching for the contracts: {ex.Message}");
			}
		}

		[HttpPut]
		[Route("v1/contract/update")]
		public async Task<IActionResult> Update([FromBody] ContractRequest request, CancellationToken cancellationToken)
		{
			try
			{
				Contract contract = _mapper.Map<Contract>(request);
				Contract? result = await _appService.Update(contract, cancellationToken);

				if (result is null)
				{
					return NotFound("An error has occurred while updating the contract, contract not found");
				}

				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest($"An error has occurred while updating the contract: {ex.Message}");
			}
		}

		[HttpDelete]
		[Route("v1/contract/delete/{id}")]
		public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
		{
			try
			{
				bool result = await _appService.Delete(id, cancellationToken);

				if (result is false)
				{
					return NotFound("Contract was not deleted, it has not been found");
				}

				return Ok("Contract has been successfully deleted");
			}
			catch (Exception ex)
			{
				return BadRequest($"An error has occurred while deleting the contract: {ex.Message}");
			}
		}
	}
}
