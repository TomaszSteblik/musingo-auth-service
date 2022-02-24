using musingo_auth_service.Models;

namespace musingo_auth_service.Data;

public interface IUsersRepository
{
    Task AddNewUser(User user);
    Task<User> GetUserByLoginId(string loginId);
    Task<User> GetUserById(Guid userId);
}