

using System.Text.Json;
using Business.Interfaces;
using Business.Models;


namespace Business.Services;





public class UserFileService : IUserFileService
{
    //Namnet på den lokala filen som användare sparas till och laddas från
    private readonly string _filePath = "users.json"; 

    public UserFileService(string filePath) 
    { 
        _filePath = filePath;

    }


    //Sparar befintligt innehåll i listan till filen
    public void SaveToFile(List<UserModel> users) 
    {
        try 
        { 
        var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
        }
        catch (Exception ex) 
        {
            throw new IOException("Ett fel uppstod när vi försökte spara användare till filen.", ex);
        }
        
    }

    public List<UserModel> LoadFromFile()
    {
        
        if (!File.Exists(_filePath))
        {
            //Returnera tom lista om filen inte finns
            return new List<UserModel>(); 
        }

        try
        {
            //Om filen finns laddas innehållet in
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
        }
            catch (Exception ex) 
            {
            throw new IOException("Ett fel uppstod när vi försökte ladda användare från filen.", ex);
            
            }
        
    }

   

}
