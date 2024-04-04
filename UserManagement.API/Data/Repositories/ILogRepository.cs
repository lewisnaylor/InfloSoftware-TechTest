using UserManagement.Models;

namespace UserManagement.API.Data.Repositories;

public interface ILogRepository
{
    Task<IQueryable<Log>> GetAllLogsAsync();

    Task<IEnumerable<Log>> GetAllLogsByUserIdAsync(long userId);

    Task CreateLogAsync(string log, long userId);
}
