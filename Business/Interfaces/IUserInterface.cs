

using Business.Models;

namespace Business.Interfaces;

public interface IUserInterface
{
    void AddUser(UserModel user);

    UserModel GetUserById(int id);

    List<UserModel> GetAllUsers();
}
