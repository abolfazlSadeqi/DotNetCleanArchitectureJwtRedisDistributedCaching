using Application.Common.Models;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.GetCustomeresWithPagination;
using Common.HelperDistributedCache;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Models;
using PublicApi.Controllers.Base;
using System;

namespace PublicAPI.Controllers.Public;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "AdminLevel2")]
public class CustomersController : ApiControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly IDistributedCache _DistributedCache;

    public CustomersController(ILogger<CustomersController> logger, IDistributedCache DistributedCache)
    {
        _logger = logger;
        _DistributedCache = DistributedCache;
    }




    [HttpGet]
    public async Task<ActionResult<PaginatedList<CustomerDto>>> GetCustomersWithPagination(
        [FromQuery] GetCustomersWithPaginationQuery query) => await _GetCustomersWithPagination(query);


    #region
    private async Task<ActionResult<PaginatedList<CustomerDto>>> _GetCustomersWithPagination([FromQuery] GetCustomersWithPaginationQuery query)
    {
        _logger.LogInformation("start_Customer");
        

        if (!_DistributedCache.TryGetValue(ListCache.CustomerCacheKey, out IEnumerable<CustomerDto>? CustomerDtos))
        {

            GetCustomersWithPaginationQuery querynew = new GetCustomersWithPaginationQuery();
            querynew.PageNumber = 1;

            querynew.PageSize = int.MaxValue;
            var _listCustomerDtoalls = Mediator.Send(querynew);

            await _DistributedCache.SetAsync(ListCache.CustomerCacheKey, _listCustomerDtoalls.Result.Items);

            _logger.LogInformation("Count_Customer =" + _listCustomerDtoalls.Result.Items.Count);

            return await Mediator.Send(query);
        }

        var item = CustomerDtos.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToList();
        PaginatedList<CustomerDto> paginatedList = new PaginatedList<CustomerDto>(
           item, CustomerDtos.Count(), query.PageNumber, query.PageSize);

        _logger.LogInformation("Count_Customer=" + item.Count);


        return new ActionResult<PaginatedList<CustomerDto>>(paginatedList);
    }

    #endregion

    [HttpGet("Get/{id:int}")]
    public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
    {
        if (!_DistributedCache.TryGetValue(ListCache.CustomerCacheKey, out IEnumerable<CustomerDto>? PersonDtos))
        {
            return await Mediator.Send(new GetCustomerQuery { Id = id });
        }

        return PersonDtos.FirstOrDefault(d => d.Id == id);


        ;
    }


    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create(CreateCustomerCommand command)
    {
        _DistributedCache.Remove(ListCache.CustomerCacheKey);
        return await Mediator.Send(command);
    }



    [HttpPut("Update")]
    public async Task<ActionResult<int>> Update(UpdateCustomerCommand command)
    {
        _DistributedCache.Remove(ListCache.CustomerCacheKey);
        return await Mediator.Send(command);


    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCustomerCommand { Id = id });
        _DistributedCache.Remove(ListCache.CustomerCacheKey);
        return Ok();
    }




}

