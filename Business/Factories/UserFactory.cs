

using Business.Models;

namespace Business.Factories;

public class UserFactory
{
    public static UserModel CreateUser(int id, string firstName, string lastName, string email, string phoneNumber, string adress, string postalCode, string city)
    {
        return new UserModel { Id = id, FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber, Adress = adress, PostalCode = postalCode, City = city
        };
    }

}
