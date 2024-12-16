

using Business.Models;
using Business.Services;

namespace Business.tests.Services;

public class UserService_Tests
{

    [Fact]
    public void AddUser_ShouldAddUserToList() 
    { 
        //Arrange
        var userService = new UserService();
        var user = new UserModel 
        {
            Id = 0,
            FirstName = "firstName",
            LastName = "lastName",
            Email = "email",
            PhoneNumber = "phoneNumber",
            Adress = "adress",
            PostalCode = "postalCode",
            City = "city"
        };

        //Act
        userService.AddUser(user);
        var users = userService.GetAllUsers();

        //Assert
        Assert.Single(users); //Testar så att endast en användare finns i listan.
        Assert.Equal("firstName", users[0].FirstName); //Testar så att användaren har samma värde i FirstName som skickades in i metoden.
    }

    [Fact]
    public void AddUser_ShouldGenerateUniqueGuidId()
    {
        //Arrange
        var userService = new UserService();
        var user = new UserModel
        {
            Id = 0,
            FirstName = "firstName",
            LastName = "lastName",
            Email = "email",
            PhoneNumber = "phoneNumber",
            Adress = "adress",
            PostalCode = "postalCode",
            City = "city"
        };

        //Act
        userService.AddUser(user);
        var generatedId = user.Id;

        //Assert
        Assert.NotEqual(0, user.Id); //Testa så att nytt ID har satts. 
        Assert.NotNull(user); //Testa så att användaren inte är null.
        Assert.True(generatedId.ToString().Length <= 4); //Testar så att det är minst 4 siffror i ID:t.
    }
        

    [Fact]
    public void GetUserById_ShouldReturnCorrectUser()
    {
        //Arrange
        var userService = new UserService();
        var user = new UserModel
        {
            Id = 0,
            FirstName = "firstName",
            LastName = "lastName",
            Email = "email",
            PhoneNumber = "phoneNumber",
            Adress = "adress",
            PostalCode = "postalCode",
            City = "city"
        };

        userService.AddUser(user);
        var id = user.Id;

        //Act
        var retrievedUser = userService.GetUserById(id);

        //Assert
        Assert.NotNull(retrievedUser); //Testar att hämtad användare inte är null
        Assert.Equal("firstName", retrievedUser.FirstName); //Testar att informationen i retrievedUser är densamma som den som skickats in i metoden.
    }

    [Fact]
    public void GetAllUsers_ShouldReturnAllUsers()
    {
        //Arrange
        var userService = new UserService();
        userService.AddUser(new UserModel { FirstName = "Christoffer", LastName = "Wen din Brink" });
        userService.AddUser(new UserModel { FirstName = "Miranda", LastName = "wen din Brink" });

        //Act
        var users = userService.GetAllUsers();

        //Assert
        Assert.Equal(2, users.Count); //Testar så att antalet användare är samma som skickats in i metoden.
        Assert.Equal("Christoffer", users[0].FirstName); //Testar så att namnet på den första användaren är samma som skickades in i metoden.
        Assert.Equal("Miranda", users[1].FirstName); //Testar så att namnet på den andra användaren är samma som skickades in i metoden.
    }

}
