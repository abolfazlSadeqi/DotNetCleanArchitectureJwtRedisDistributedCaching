using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class GetCustomerQuery : IRequest<CustomerDto>
{
    public int Id { get; set; }
}

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(request.Id);

        return _mapper.Map<Customer, CustomerDto>(entity!);
    }
}

