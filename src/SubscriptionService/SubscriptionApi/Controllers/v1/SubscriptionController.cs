using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SubscriptionApi.Dtos.Requests;
using SubscriptionApi.Dtos.Responses;
using SubscriptionApplication.Abstractions.AppServices;
using SubscriptionDomain.Entities;

namespace SubscriptionApi.Controllers.v1
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionAppService _appService;
        private readonly IMapper _mapper;

        public SubscriptionController(ISubscriptionAppService appService, IMapper mapper)
        {
            _appService = appService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("v1/subscription/add")]
		[ProducesResponseType(typeof(SubscriptionResponse), 200),
		 ProducesResponseType(500)]
		public async Task<IActionResult> Add([FromBody] SubscriptionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Subscription subscription = _mapper.Map<Subscription>(request);
                await _appService.Add(subscription, cancellationToken);

                return Ok(_mapper.Map<SubscriptionResponse>(subscription));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error ocurred while adding the subscription {ex.Message}");
            }
        }

        [HttpGet]
        [Route("v1/subscription/get/{id}")]
		[ProducesResponseType(typeof(SubscriptionResponse),200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                Subscription? subscription = await _appService.Get(id, cancellationToken);

                if (subscription is null)
                {
                    return NotFound("Subscription not found");
                }

                return Ok(_mapper.Map<SubscriptionResponse>(subscription));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has occurred while searching for the subscription: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("v1/subscription/get")]
		[ProducesResponseType(typeof(List<SubscriptionResponse>), 200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                List<Subscription> subscriptions = await _appService.Get(cancellationToken);

                if (subscriptions.Count == 0)
                {
                    return NotFound("No subscriptions has been found");
                }

                return Ok(_mapper.Map<List<SubscriptionResponse>>(subscriptions));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has occurred while searching for the subscription: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("v1/subscription/update")]
		[ProducesResponseType(typeof(SubscriptionResponse), 200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Update([FromBody] SubscriptionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Subscription subscription = _mapper.Map<Subscription>(request);
                Subscription? result = await _appService.Update(subscription, cancellationToken);

                if (result is null)
                {
                    return NotFound("An error has occurred while updating the subscription, subscription not found");
                }

                return Ok(_mapper.Map<SubscriptionResponse>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has occurred while updating the subscription: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("v1/subscription/delete/{id}")]
		[ProducesResponseType (200),
		 ProducesResponseType(404), ProducesResponseType(500)]
		public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                bool result = await _appService.Delete(id, cancellationToken);

                if (result is false)
                {
                    return NotFound("Subscription was not deleted, it has not been found");
                }

                return Ok("Subscription has been successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An error has occurred while deleting the subscription: {ex.Message}");
            }
        }
    }
}
