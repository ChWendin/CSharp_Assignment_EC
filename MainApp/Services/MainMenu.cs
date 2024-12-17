


using Business.Interfaces;

using Business.Models;
using Business.Services;

namespace Console_MainApp.Services;

public class MainMenu
{
    private readonly IUserInterface _userService;
    private readonly UserFileService _fileService;
    private readonly List<UserModel> _users;
    private readonly SubMenus _subMenus;



    public MainMenu(IUserInterface userService, UserFileService fileService)
    {
        _userService = userService;
        _fileService = fileService;
        
        //Laddar in filen vid programstart
        _users = _fileService.LoadFromFile();
        _subMenus = new SubMenus(_userService, _fileService);
    }
    
    public void ShowMenu()
    {
        
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Huvudmeny ---");
            Console.WriteLine("1. Lägg till användare");
            Console.WriteLine("2. Visa alla användare");
            Console.WriteLine("3. Uppdatera/Radera användare");
            Console.WriteLine("4. Radera alla användare");
            Console.WriteLine("5. Avsluta");
            Console.Write("Välj ett alternativ: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    _subMenus.AddUserMenu();
                    
                    break;
                case "2":
                    _subMenus.ShowAllUsersMenu();
                    break;
                case "3":
                    _subMenus.UpdateUserMenu();
                    break;
                case "4":
                    _subMenus.DeleteAllUsersMenu();
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







}
