

using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class UserService : IUserInterface
{
    // Privat lista för att lagra användare
    private readonly List<UserModel> _users;

    // Konstruktor för att initiera listan
    public UserService()
    {
        _users = new List<UserModel>();
    }

    // Lägg till en användare i listan
    public void AddUser(UserModel user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "Användaren kan inte vara null.");
        }

        // Sätt unikt ID om det inte redan är satt
        if (user.Id == 0)
        {
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
        }

        _users.Add(user);
    }

    // Hämta en användare baserat på ID
    public UserModel GetUserById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            throw new KeyNotFoundException($"Ingen användare hittades med ID: {id}");
        }

        return user;
    }

    // Hämta alla användare
    public List<UserModel> GetAllUsers()
    {
        return _users.ToList(); // Returnerar en kopia av listan för att förhindra direkt manipulation
    }
}
