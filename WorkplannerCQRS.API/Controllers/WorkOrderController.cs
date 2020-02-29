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
        /// Creates a new WorkOrder
        /// </summary>
        /// <param name="command">Create work order command</param>
        /// <returns>Response for the request</returns>
        /// <response code="201">Returns the location of the newly created work order</response>
        /// <response code="400">Return data containing information about why has the request failed</response>
        [HttpPost(Name = "CreateWorkOrder")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] CreateWorkOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByObjectNumber), new { Id = result.Data.ObjectNumber }, result);
        }
        
        /// <summary>
        /// Gets all work orders
        /// </summary>
        /// <response code="200">Return all work orders in the database</response>
        /// <response code="204">Return NoContent if no records were found in the database</response>
        /// <returns>Response with all work orders</returns>
        [HttpGet(Name = "GetAllWorkOrders")]
        [ProducesResponseType(typeof(ApiResponse<List<WorkOrderResponse>>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllWorkOrdersQuery());
            return result.Data.Any() ? (IActionResult) Ok(result.Data) : NoContent();
        }

        /// <summary>
        /// Gets work order by object number
        /// </summary>
        /// <param name="objectNumber"></param>
        /// <returns>Returns work order with given object number, or 404 if not found</returns>
        /// <response code="200">Returns work order with the given object number</response>
        /// <response code="404">Returns NotFound http status code</response>
        [HttpGet(Name = "GetWorkOrderByObjectNumber")]
        [Route("{objectNumber}")]
        [ProducesResponseType(typeof(ApiResponse<WorkOrderResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByObjectNumber(string objectNumber)
        {
            var query = new GetWorkOrderByIdQuery(objectNumber);
            var result = await _mediator.Send(query);
            return result.Success ? (IActionResult) Ok(result.Data) : NotFound();
        }

        /// <summary>
        /// Update updates a given work order
        /// </summary>
        /// <param name="id">Id of work order entity</param>
        /// <param name="command">Update command for the work order entity</param>
        /// <returns>Returns updated worker entity response</returns>
        /// <response code="200">Returns updated work order entity response</response>
        /// <response code="404">Returns 404 NotFound if work order with given id cannot be found</response>
        [HttpPut(Name = "UpdateWorkOrder")]
        [Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<WorkOrderResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateWorkOrderCommand command)
        {
            command.ObjectNumber = id; 
            var result = await _mediator.Send(command);
            return result.Success ? (IActionResult) Ok(result) : NotFound();
        }
        
        /// <summary>
        /// Update updates worker
        /// </summary>
        /// <param name="id">Id of work order entity</param>
        /// <returns>Returns updated work order entity response</returns>
        /// <response code="200">Returns updated work order entity response</response>
        /// <response code="404">Returns 404 NotFound if work order with given id cannot be found</response>
        [HttpDelete(Name = "DeleteWorkOrder")]
        [Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<WorkOrderResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] string id)
        {
            var command = new DeleteWorkOrderCommand(id);
            var result = await _mediator.Send(command);
            return result.Success ? (IActionResult) Ok(result) : NotFound();
        }
    }
}