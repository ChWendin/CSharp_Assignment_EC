


using Business.Interfaces;
using Business.Factories;
using Business.Models;
using Business.Services;

namespace Console_MainApp.Services;

public class MainMenu
{
    private readonly IUserInterface _userService;
    private readonly UserFileService _fileService;
    private readonly List<UserModel> _users;
    

    public MainMenu(IUserInterface userService, UserFileService fileService)
    {
        _userService = userService;
        _fileService = fileService;
        _users = _fileService.LoadFromFile();
    }
    
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Huvudmeny ---");
            Console.WriteLine("1. Lägg till användare");
            Console.WriteLine("2. Visa alla användare");
            Console.WriteLine("3. Uppdatera användare");
            Console.WriteLine("4. Radera alla användare");
            Console.WriteLine("5. Avsluta");
            Console.Write("Välj ett alternativ: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AddUser();
                    break;
                case "2":
                    ShowAllUsers();
                    break;
                case "3":
                    //UpdateUser();
                    break;
                case "4":
                    DeleteAllUsers();
                    break;
                case "5":
                    Console.WriteLine("Avslutar...");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void AddUser()
    {
        Console.Clear();
        Console.WriteLine("Ange användarens förnamn: ");
        string firstName = Console.ReadLine()!;

        Console.WriteLine("Ange användarens efternamn: ");
        string lastName = Console.ReadLine()!;

        Console.WriteLine("Ange användarens E-post: ");
        string email = Console.ReadLine()!;

        Console.WriteLine("Ange användarens telefonnummer: ");
        string phoneNumber = Console.ReadLine()!;

        Console.WriteLine("Ange användarens adress: ");
        string adress = Console.ReadLine()!;

        Console.WriteLine("Ange användarens postnummer: ");
        string postalCode = Console.ReadLine()!;

        Console.WriteLine("Ange användarens bostadsort: ");
        string city = Console.ReadLine()!;

       

        try
        {

            UserModel newUser = UserFactory.CreateUser(0, firstName, lastName, email, phoneNumber, adress, postalCode, city);

            _userService.AddUser(newUser);
            //Lägg till användare i listan
            _users.Add(newUser);
            // Spara listan till fil
            _fileService.SaveToFile(_users); 
            Console.WriteLine($"Användaren {newUser.FirstName} {newUser.LastName} (ID: {newUser.Id}) har lagts till.");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
    }

    private void ShowAllUsers()
    {
        var users = _users;
        if (users.Count == 0)
        {
            Console.WriteLine("Det finns inga användare.");
            Console.ReadKey();
            return;
        }
        //Visar alla användare 
        Console.WriteLine("\n--- Alla användare ---");
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id} First name: {user.FirstName}, Last name: {user.LastName}, E-mail: {user.Email}");
            Console.WriteLine($"Phone number: {user.PhoneNumber}");
            Console.WriteLine($"Adress: {user.Adress} Postal code: {user.PostalCode} City: {user.City}");
            Console.WriteLine("");
        }
        Console.ReadKey();
    }

    private void DeleteAllUsers() 
    { 
        Console.Clear();
        Console.WriteLine("Är du säker på att du vill radera alla användare? (ja/nej)");
        var input = Console.ReadLine();
        if (input!.ToLower() == "ja")
        {
            _users.Clear();
            _fileService.SaveToFile(_users);
            Console.WriteLine("Alla användare har nu raderats");
            
        }
        else 
        { 
            Console.WriteLine("Inga användare har raderats.");
        }
        Console.ReadKey();
    }


}
