

using Business.Interfaces;
using Business.Models;



namespace Business.Services;

public class UserService : IUserInterface
{
    //Lista för att lagra användare
    private readonly List<UserModel> _users;

    //Konstruktor för att initiera listan
    public UserService()
    {
        _users = new List<UserModel>();
    }

    //Lägger till en användare i listan
    public void AddUser(UserModel user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "Användaren kan inte vara null.");
        }

        //Sätter ett unikt ID 
        if (user.Id == 0)
        {
            user.Id = GenerateUniqueId();
        }

        _users.Add(user);
    }

    private int GenerateUniqueId()
    {
        int id;
        do
        {
            //Genererar ett GUID och tar de 4 sista siffrorna från hashkoden
              id = Math.Abs(Guid.NewGuid().GetHashCode() % 10000);
            //Säkerställer att ID är unikt
        } while (_users.Any(u => u.Id == id)); 

        return id;
    }


    //Hämta en användare baserat på ID
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
        return _users.ToList(); //Returnerar en kopia av listan för att förhindra att den kan ändras på olika ställen
    }

    
}
