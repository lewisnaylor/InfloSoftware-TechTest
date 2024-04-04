using MediatR;
using UserManagement.API.Data.Repositories;

namespace UserManagement.API.Features.User;

public class Index
{
    public class Query : IRequest<Result>
    {
        public long Id { get; set; }
    }

    public class Result
    {
        public UserManagement.Models.User User { get; set; }
    }

    public class QueryHandler : IRequestHandler<Query, Result>
    {
        private readonly UserRepository _userRepository;

        public QueryHandler(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            return new Result
            {
                User = await _userRepository.GetUserAsync(request.Id)
            };
        }
    }
}
