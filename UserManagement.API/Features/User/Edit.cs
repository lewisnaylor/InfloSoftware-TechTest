using MediatR;
using UserManagement.API.Data.Repositories;

namespace UserManagement.API.Features.User;

public class Edit
{
    public class Command : IRequest<Unit>
    {
        public UserManagement.Models.User User { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command, Unit>
    {
        private readonly UserRepository _userRepository;
        private readonly ILogRepository _logger;

        public CommandHandler(UserRepository userRepository, ILogRepository logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            await _userRepository.EditUserAsync(request.User);
            await _logger.CreateLogAsync($"Edited User {request.User.Forename} {request.User.Surname}", request.User.Id);
            return Unit.Value;
        }
    }
}
