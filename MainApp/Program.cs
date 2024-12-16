
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Business.Interfaces;
using Business.Services;
using Console_MainApp.Services;

class Program
{
    static void Main(string[] args)
    { 
        //Skapa en host för att konfigurera DI
        IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                //Registrera tjänster
                services.AddSingleton<IUserInterface, UserService>();
                services.AddSingleton<UserFileService>(provider => new UserFileService("users.json"));
                services.AddSingleton<MainMenu>();
            })
            .Build();

        //Kör programmet
        var mainMenu = host.Services.GetRequiredService<MainMenu>();
        mainMenu.ShowMenu();
    }
   
      
}
