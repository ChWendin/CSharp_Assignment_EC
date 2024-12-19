

using Business.Models;

namespace Business.Interfaces
{
    public interface IUserFileService
    {
        List<UserModel> LoadFromFile();
        void SaveToFile(List<UserModel> users);
    }
}
