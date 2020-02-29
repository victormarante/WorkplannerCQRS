using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkplannerCQRS.API.Domain;
using WorkplannerCQRS.API.Domain.Worker.Commands;
using WorkplannerCQRS.API.Domain.Worker.DTOs;
using WorkplannerCQRS.API.Domain.Worker.Queries;

namespace WorkplannerCQRS.API.Controllers
{
    public class WorkersController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public WorkersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Post creates a new Worker
        /// </summary>
        /// <param name="command">Create worker command</param>
        /// <returns>Response for the request</returns>
        /// <response code="201">Returns the location of the newly created worker</response>
        /// <response code="400">Return data containing information about why has the request failed</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]CreateWorkerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { Id = result.Data.WorkerId }, result);
        }
        
        /// <summary>
        /// GetById gets worker by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns worker with given id, or 404 if not found</returns>
        /// <response code="200">Returns worker with the given object number</response>
        /// <response code="404">Returns NotFound http status code</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var query = new GetWorkerByIdQuery(id);
            var result = await _mediator.Send(query);
            return result.Success ? (IActionResult) Ok(result.Data) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<WorkerResponse>>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllWorkersQuery());
            return result.Data.Any() ? (IActionResult) Ok(result) : NoContent();
        }
    }
}