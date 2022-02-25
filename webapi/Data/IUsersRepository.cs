using musingo_auth_service.Models;

namespace musingo_auth_service.Data;

public interface IUsersRepository
{
    Task AddNewUserAsync(User user);
    Task<User> GetUserByLoginIdAsync(string loginId);
    Task<User> GetUserByIdAsync(Guid userId);
    Task<bool> UpdateUserAsync(User user);
}