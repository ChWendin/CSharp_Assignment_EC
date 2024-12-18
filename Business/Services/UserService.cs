﻿


using Business.Interfaces;
using Business.Models;



namespace Business.Services;

public class UserService : IUserInterface
{

    //Lista för att lagra användare
    private readonly List<UserModel> _users;
    private readonly IUserFileService _fileService;


    //Konstruktor för att initiera listan
    public UserService(IUserFileService fileService)
    {
        _fileService = fileService;
        _users = _fileService.LoadFromFile();

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

        //Lägg till användaren i listan
            _users.Add(user);
        
        try
        {
            
            //Sparar den lokala listan till fil
            _fileService.SaveToFile(_users);
            
        }

            

        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
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

    public void DeleteUser(int id)
    {
        var user = GetUserById(id);

        _users.Remove(user);

        // Spara uppdaterad lista till fil
        _fileService.SaveToFile(_users);
    }

    public void UpdateUser(int id, string? firstName, string? lastName, string? email, string? phoneNumber, string? address, string? postalCode, string? city) 
    {
        var user = GetUserById(id);

        if (user == null) throw new Exception("Användaren hittades inte.");

        //Om inget fylls i så behålls det gamla värdet.
        user.FirstName = string.IsNullOrWhiteSpace(firstName) ? user.FirstName : firstName;
        user.LastName = string.IsNullOrWhiteSpace(lastName) ? user.LastName : lastName;
        user.Email = string.IsNullOrWhiteSpace(email) ? user.Email : email;
        user.PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? user.PhoneNumber : phoneNumber;
        user.Adress = string.IsNullOrWhiteSpace(address) ? user.Adress : address;
        user.PostalCode = string.IsNullOrWhiteSpace(postalCode) ? user.PostalCode : postalCode;
        user.City = string.IsNullOrWhiteSpace(city) ? user.City : city;

        _fileService.SaveToFile(_users);
    }

    public void DeleteAllUsers()
    {
        _users.Clear();

        // Spara tom lista till fil
        _fileService.SaveToFile(_users);
    }

    public void SaveUsersToFile()
    {
        _fileService.SaveToFile(_users);
    }


}
          


