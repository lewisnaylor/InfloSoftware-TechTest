using MediatR;
using UserManagement.API.Data.Repositories;

namespace UserManagement.API.Features.UserList;

public class Index
{
    public class Query : IRequest<Result>
    {
        public Data.Repositories.UserRepository.ActiveFilter ActiveFilter { get; set; }
    }

    public class Result
    {
        public IQueryable<UserManagement.Models.User> Users { get; set; }
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
                Users = await _userRepository.GetAllUsersAsync(request.ActiveFilter)
            };
        }
    }
}
