using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItems;

public record GetTodoItemsQuery : IRequest<List<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, List<TodoItemBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TodoItemBriefDto>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .OrderBy(x => x.Title)
            .ProjectTo<TodoItemBriefDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}
