using Luftborn.Services.CustomerService.Command;
using Luftborn.Services.CustomerService.Commands;
using Luftborn.Services.CustomerService.Handlers;
using Luftborn.Services.CustomerService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Luftborn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <response code="201">Customer created successfully</response>
        /// <response code="400">Invalid input data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateCustomerCommend command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateCustomerCommand command)
        {
         
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Deletes a customer by ID
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand() { Id = id});
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all customers with optional filtering
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCustomersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
