using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkplannerCQRS.API.Domain;
using WorkplannerCQRS.API.Domain.WorkOrder.Commands;
using WorkplannerCQRS.API.Domain.WorkOrder.DTOs;
using WorkplannerCQRS.API.Domain.WorkOrder.Queries;

namespace WorkplannerCQRS.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WorkOrderController : ApiBaseController
    {
        private readonly IMediator _mediator;
        
        public WorkOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Gets all work orders
        /// </summary>
        /// <response code="200">Return all work orders in the database</response>
        /// <response code="204">Return NoContent if no records were found in the database</response>
        /// <returns>Response with all work orders</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiResponse<WorkOrderResponse>>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllWorkOrdersQuery());
            return result.Data.Any() ? (IActionResult) Ok(result) : NoContent();
        }

        /// <summary>
        /// Gets work order by object number
        /// </summary>
        /// <param name="objectNumber"></param>
        /// <returns>Returns work order with given object number, or 404 if not found</returns>
        /// <response code="200">Returns work order with the given object number</response>
        /// <response code="404">Returns NotFound http status code</response>
        [HttpGet]
        [Route("{objectNumber}")]
        [ProducesResponseType(typeof(ApiResponse<WorkOrderResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByObjectNumber(string objectNumber)
        {
            var query = new GetWorkOrderByIdQuery(objectNumber);
            var result = await _mediator.Send(query);
            return result == null ? (IActionResult) NotFound() : Ok(result);
        }

        /// <summary>
        /// Creates a new WorkOrder
        /// </summary>
        /// <param name="command">Create work order command</param>
        /// <returns>Response for the request</returns>
        /// <response code="201">Returns the location of the newly created work order</response>
        /// <response code="400">Return data containing information about why has the request failed</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] CreateWorkOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByObjectNumber), new { Id = result.Data.ObjectNumber }, result);
        }
    }
}