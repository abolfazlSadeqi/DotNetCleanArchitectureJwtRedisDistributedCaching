using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Customers.Queries.GetCustomeresWithPagination;
using Application.Customers.Queries.GetCustomeresWithPagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class GetCustomersWithPaginationQuery : IRequest<PaginatedList<CustomerDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetCustomersWithPaginationQueryHandler : IRequestHandler<GetCustomersWithPaginationQuery, PaginatedList<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CustomerDto>> Handle(GetCustomersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers.AsNoTracking()
            .OrderBy(x => x.Firstname)
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
