using System;
using System.Collections.Generic;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.API.Data.Repositories;

public class UserRepository
{
    private readonly IDataContext _dataAccess;
    public UserRepository(IDataContext dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public enum ActiveFilter
    {
        Active = 0,
        Inactive = 1,
        All = 2
    }

    public async Task<IQueryable<User>> GetAllUsersAsync(ActiveFilter activeFilter)
    {
        var users = await _dataAccess.GetAll<User>();
        return users.Where(x => activeFilter == ActiveFilter.Active && x.IsActive == true
                            || activeFilter == ActiveFilter.Inactive && x.IsActive == false
                            || activeFilter == ActiveFilter.All);
    }

    public async Task<User> GetUserAsync(long id)
    {
        var result = await _dataAccess.GetAll<User>();
        return result.FirstOrDefault(x => x.Id == id);
    }

    public async Task CreateUserAsync(User user)
    {
        await _dataAccess.CreateAsync<User>(user);
    }

    public async Task EditUserAsync(User user)
    {
        await _dataAccess.UpdateAsync<User>(user);
    }

    public async Task DeleteUserAsync(User user)
    {
        await _dataAccess.DeleteAsync<User>(user);
    }
}
