using MediatR;
using UserManagement.API.Data.Repositories;

namespace UserManagement.API.Features.Log;

public class Index
{
    public class Query : IRequest<Result>
    {
        public long? UserId { get; set; }
    }

    public class Result
    {
        public IEnumerable<Models.Log> Logs { get; set; } = default!;
    }

    public class QueryHandler : IRequestHandler<Query, Result>
    {
        private readonly ILogRepository _logger;

        public QueryHandler(ILogRepository logger)
        {
            _logger = logger;
        }

        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = request.UserId.HasValue
                ? await _logger.GetAllLogsByUserIdAsync(request.UserId.Value)
                : await _logger.GetAllLogsAsync();
            return new Result
            {
                Logs = result.OrderByDescending(x => x.CreatedDate)
            };
        }
    }
}
