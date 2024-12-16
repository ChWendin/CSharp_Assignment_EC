

using Business.Factories;
using Business.Models;

namespace Business.tests.Factories;

public class UserFactory_Tests
{
    [Fact]
    public void CreateUser_ShouldReturnUserModel() 
    {
        //Arrange
        int id = 1;
        string firstName = "";
        string lastName = "";
        string email = "email";
        string phoneNumber = "1234567890";
        string adress = "Gatan22";
        string postalCode = "12345";
        string city = "Göteborg";


        //Act
        UserModel result = UserFactory.CreateUser(id, firstName, lastName, email, phoneNumber, adress, postalCode, city);

        //Assert
        Assert.IsType<UserModel>(result); //Testar att jag får ett objekt som är en UserModel 
        Assert.NotNull(result); //Testar att resultatet inte är null.
        
    }
}


        
