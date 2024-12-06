


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
                    UpdateUser();
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

    private void UpdateUser() 
    {
        Console.Clear();
        Console.WriteLine("Ange ID på den användare du vill söka efter: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Ogiltigt ID. Försök igen.");
            Console.ReadKey();
            return;
        }

        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            Console.WriteLine($"Ingen användare med ID {userId} hittades.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Hittad användare:");
        Console.WriteLine($"ID: {user.Id} First name: {user.FirstName}, Last name: {user.LastName}, E-mail: {user.Email}");
        Console.WriteLine($"Phone number: {user.PhoneNumber}");
        Console.WriteLine($"Adress: {user.Adress} Postal code: {user.PostalCode} City: {user.City}");
        Console.WriteLine("");
        Console.WriteLine("Vill du [1] Uppdatera eller [2] Radera denna användare?");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            // Uppdatera användare
            Console.WriteLine("Ange nytt förnamn (lämna tomt för att behålla befintligt): ");
            string newFirstName = Console.ReadLine()!;
            Console.WriteLine("Ange nytt efternamn (lämna tomt för att behålla befintligt): ");
            string newLastName = Console.ReadLine()!;
            Console.WriteLine("Ange ny E-post (lämna tomt för att behålla befintligt): ");
            string newEmail = Console.ReadLine()!;
            Console.WriteLine("Ange nytt telefonnummer (lämna tomt för att behålla befintligt): ");
            string newPhoneNumber = Console.ReadLine()!;
            Console.WriteLine("Ange ny adress (lämna tomt för att behålla befintligt): ");
            string newAdress = Console.ReadLine()!;
            Console.WriteLine("Ange nytt postnummer (lämna tomt för att behålla befintligt): ");
            string newPostalCode = Console.ReadLine()!;
            Console.WriteLine("Ange ny bostadsort (lämna tomt för att behålla befintligt): ");
            string newCity = Console.ReadLine()!;

            user.FirstName = string.IsNullOrWhiteSpace(newFirstName) ? user.FirstName : newFirstName;
            user.LastName = string.IsNullOrWhiteSpace(newLastName) ? user.LastName : newLastName;
            user.Email = string.IsNullOrWhiteSpace(newEmail) ? user.Email : newEmail;
            user.PhoneNumber = string.IsNullOrWhiteSpace(newPhoneNumber) ? user.PhoneNumber : newPhoneNumber;
            user.Adress = string.IsNullOrWhiteSpace(newAdress) ? user.Adress : newAdress;
            user.PostalCode = string.IsNullOrWhiteSpace(newPostalCode) ? user.PostalCode : newPostalCode;
            user.City = string.IsNullOrWhiteSpace(newCity) ? user.City : newCity;

            Console.WriteLine($"Användaren med ID {user.Id} har uppdaterats.");
        }
        else if (choice == "2")
        {
            // Radera användare
            _users.Remove(user);
            Console.WriteLine($"Användaren med ID {user.Id} har raderats.");
        }
        else
        {
            Console.WriteLine("Ogiltigt val. Ingen ändring har gjorts.");
        }

        // Spara ändringarna till fil
        _fileService.SaveToFile(_users);
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
