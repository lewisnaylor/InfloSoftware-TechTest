using System;
using System.Collections.Generic;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.API.Data.Repositories;

public class LogRepository : ILogRepository
{
    private readonly IDataContext _dataAccess;

    public LogRepository(IDataContext dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<IQueryable<Log>> GetAllLogsAsync()
    {
        return await _dataAccess.GetAll<Log>();
    }

    public async Task<IEnumerable<Log>> GetAllLogsByUserIdAsync(long userId)
    {
        var result = await _dataAccess.GetAll<Log>();
        return result.Where(x => x.UserId == userId);
    }

    public async Task CreateLogAsync(string log, long userId)
    {
        await _dataAccess.CreateAsync(new Log
        {
            UserId = userId,
            CreatedDate = DateTime.Now,
            LogDescription = log
        });
    }
}
