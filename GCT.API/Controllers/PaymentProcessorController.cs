using GCT.Contracts.DTO;
using GCT.Core.Exceptions;
using GCT.Core.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GCT.API.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class PaymentProcessorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentProcessorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Deposint an Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TopUp")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> TopUp([FromBody] TopUpAccountDTO model)
        {
            try
            {
                var command = new TopUpRecipientCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }


        /// <summary>
        /// Deposint an Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Deposit")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Deposit([FromBody] DepositAccountDTO model)
        {
            try
            {
                var command = new DepositAccountCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        /// <summary>
        /// Withdrawal from Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Withdrawal")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Withdrawal([FromBody] WithdrawalAccountDTO model)
        {
            try
            {
                var command = new WithrawalAccountCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }
    }
}
