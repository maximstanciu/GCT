using GCT.Contracts.DTO;
using GCT.Core.Exceptions;
using GCT.Core.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GCT.API.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class LedgerController : Controller
    {
        private readonly IMediator _mediator;

        public LedgerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Show list ot Transactions by Account Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{accountId}/Account")]
        [ProducesResponseType(typeof(IEnumerable<TransactionDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetByAccountId(int accountId)
        {
            try
            {
                var query = new GetTransactionsByQuery(accountId, null);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }

        /// <summary>
        /// Show list ot Transactions by Account Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}/User")]
        [ProducesResponseType(typeof(IEnumerable<TransactionDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var query = new GetTransactionsByQuery(null, userId);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }
    }
}
