


using Business.Interfaces;
using Business.Models;
using Business.Services;



namespace Console_MainApp.Services;

public class SubMenus
{
    private readonly IUserInterface _userService;
    private readonly UserFileService _fileService;
   

    public SubMenus(IUserInterface userService, UserFileService fileService)
    {
        _userService = userService;
        _fileService = fileService;
        
    }

    public UserModel AddUserMenu()
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

        var newUser = new UserModel() 
        {
            Id = 0, //ID kommer sättas av UserService
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Adress = adress,
            PostalCode = postalCode,
            City = city
        };

        if (newUser != null)
        {
            _userService.AddUser(newUser);
           
            Console.WriteLine($"Användaren {newUser.FirstName} {newUser.LastName} har lagts till.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Ingen användare lades till.");
            Console.ReadKey();
        }

        return newUser!;
    }

    public void ShowAllUsersMenu()
    {
        
        var users = _userService.GetAllUsers();
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

    public void UpdateUserMenu()
    {
        Console.Clear();
        Console.WriteLine("Ange ID på den användare du vill söka efter: ");

        //Om sökningen är i felaktigt format så skickas felmeddelande
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Ogiltigt ID. Försök igen.");
            Console.ReadKey();
            return;
        }


        //Söker igenom listan efter användare på ID och visar hela användaren om man får träff på sökningen. Annars får användaren ett felmeddelande.
        var user = _userService.GetUserById(userId);
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
            //Uppdaterar användare
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

            //Om inget fylls i så behålls det gamla värdet.
            user.FirstName = string.IsNullOrWhiteSpace(newFirstName) ? user.FirstName : newFirstName;
            user.LastName = string.IsNullOrWhiteSpace(newLastName) ? user.LastName : newLastName;
            user.Email = string.IsNullOrWhiteSpace(newEmail) ? user.Email : newEmail;
            user.PhoneNumber = string.IsNullOrWhiteSpace(newPhoneNumber) ? user.PhoneNumber : newPhoneNumber;
            user.Adress = string.IsNullOrWhiteSpace(newAdress) ? user.Adress : newAdress;
            user.PostalCode = string.IsNullOrWhiteSpace(newPostalCode) ? user.PostalCode : newPostalCode;
            user.City = string.IsNullOrWhiteSpace(newCity) ? user.City : newCity;

            _userService.SaveUsersToFile();

            Console.WriteLine($"Användaren med ID {user.Id} har uppdaterats.");
        }
        else if (choice == "2")
        {
            try
            {
                _userService.DeleteUser(user.Id); // Lägg till en metod i IUserInterface om den saknas
                Console.WriteLine($"Användaren med ID {user.Id} har raderats.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid radering av användaren: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ogiltigt val. Ingen ändring har gjorts.");
        }
       
        Console.ReadKey();
    }

    //Raderar samtliga användare i listan och uppdaterar filen.
    public void DeleteAllUsersMenu()
    {
        Console.Clear();
        Console.WriteLine("Är du säker på att du vill radera alla användare? (ja/nej)");
        var input = Console.ReadLine();
        if (input!.ToLower() == "ja")
        {
            try
            {
                _userService.DeleteAllUsers();
                Console.WriteLine("Alla användare har nu raderats.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid radering av användarna: {ex.Message}");
            }

        }
        else
        {
            Console.WriteLine("Inga användare har raderats.");
        }
        Console.ReadKey();
    }


}
