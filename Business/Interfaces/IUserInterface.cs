

using Business.Models;

namespace Business.Interfaces;

public interface IUserInterface
{
    void AddUser(UserModel user);

    UserModel GetUserById(int id);

    List<UserModel> GetAllUsers();

    void DeleteUser(int id);

    void UpdateUser(int id, string? firstName, string? lastName, string? email, string? phoneNumber, string? address, string? postalCode, string? city);
    void DeleteAllUsers();

    void SaveUsersToFile();
}
