
using Business.Interfaces;
using Business.Services;
using Console_MainApp.Services;

class Program
{
    static void Main(string[] args)
    { 
        IUserInterface userInterface = new UserService();
        var fileService = new UserFileService("users.json");
        
        var menu = new MainMenu(userInterface, fileService);

        menu.ShowMenu();
    }
}
