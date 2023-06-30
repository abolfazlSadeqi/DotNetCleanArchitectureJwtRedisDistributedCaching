using Application.Common.Models;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.GetCustomeresWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PublicApi.Controllers.Base;

namespace PublicAPI.Controllers.Public;

public class CustomersController : ApiControllerBase
{
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<CustomerDto>>> GetCustomersWithPagination([FromQuery] GetCustomersWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("Get/{id:int}")]
    public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
    {
        return await Mediator.Send(new GetCustomerQuery { Id = id });
    }


    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create(CreateCustomerCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("Update")]
    public async Task<ActionResult<int>> Update( UpdateCustomerCommand command)
    {
        return await Mediator.Send(command);
    

    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCustomerCommand { Id = id });

        return NoContent();
    }
}

