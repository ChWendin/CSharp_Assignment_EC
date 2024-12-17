

using System.Text.Json;
using Business.Models;


namespace Business.Services;

public class UserFileService
{
    //Namnet på den lokala filen som användare sparas till och laddas från
    private readonly string _filePath = "users.json"; 

    public UserFileService(string filePath) 
    { 
        _filePath = filePath;
        Console.WriteLine($"FilePath som används i UserFileService: {_filePath}");
        Console.ReadKey();
    }


    //Sparar befintligt innehåll i listan till filen
    public void SaveToFile(List<UserModel> users) 
    {
        try 
        { 
        var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
        }
        catch 
        {
            Console.WriteLine("Något gick fel! Vi kunde inte spara innehållet till filen.");
            Console.ReadKey();
        }
        
    }

    public List<UserModel> LoadFromFile()
    {
        
        if (!File.Exists(_filePath))
        {
            // Returnera tom lista om filen inte finns
            return new List<UserModel>(); 
        }

        try
        {
            //Om filen finns laddas innehållet in
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
        }
            catch 
            {   
                Console.WriteLine("Vi kunde inte ladda filen!");
                Console.ReadKey();
                return new List<UserModel>();
            }
        
    }

   

}
