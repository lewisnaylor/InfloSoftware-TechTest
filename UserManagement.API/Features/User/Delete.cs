using MediatR;
using UserManagement.API.Data.Repositories;

namespace UserManagement.API.Features.User;

public class Delete
{
    public class Query : IRequest<Unit>
    {
        public Models.User User { get; set; }
    }

    public class QueryHandler : IRequestHandler<Query, Unit>
    {
        private readonly UserRepository _userRepository;
        private readonly ILogRepository _logger;

        public QueryHandler(UserRepository userRepository, ILogRepository logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteUserAsync(request.User);
            await _logger.CreateLogAsync($"Deleted User {request.User.Forename} {request.User.Surname}", request.User.Id);
            return Unit.Value;
        }
    }
}
