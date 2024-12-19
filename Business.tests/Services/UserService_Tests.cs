

using Business.Models;
using Business.Services;
using Moq;
using Business.Interfaces;

namespace Business.tests.Services;

public class UserService_Tests
{
    [Fact]
    public void AddUser_ShouldAddUserToList_WhenValidUserIsProvided() 
    { 
        //Arrange
        var mockFileService = new Mock<IUserFileService>();
        mockFileService.Setup(m => m.LoadFromFile()).Returns(new List<UserModel>());
        var userService = new UserService(mockFileService.Object);

        var newUser = new UserModel { FirstName = "Test", LastName = "Testsson"};

        //Act
        userService.AddUser(newUser);

        //Assert
        Assert.Single(userService.GetAllUsers());
        Assert.NotEqual(0, newUser.Id);
        mockFileService.Verify(m => m.SaveToFile(It.IsAny<List<UserModel>>()), Times.Once);
    }


    [Fact]
    public void GetUserByID_ShouldReturnUser_WithCorrectId() 
    {
        //Arrange
        var mockFileService = new Mock<IUserFileService>();
        mockFileService.Setup(m => m.LoadFromFile()).Returns(new List<UserModel>());
        var userService = new UserService(mockFileService.Object);

        var user = new UserModel { Id = 1, FirstName = "Test", LastName = "Testsson" };
        userService.AddUser(user);

        //Act
        userService.GetUserById(user.Id);

        //Assert
        Assert.Single(userService.GetAllUsers());
        Assert.Equal("Test", user.FirstName);
        Assert.Equal("Testsson", user.LastName);
        Assert.Equal(1, user.Id);
    }

    [Fact]
    public void GetAllUsers_ShouldReturnAllUsers_FromList()
    {
        //Arrange
        var mockFileService = new Mock<IUserFileService>();
        mockFileService.Setup(m => m.LoadFromFile()).Returns(new List<UserModel>());
        var userService = new UserService(mockFileService.Object);

        var user1 = new UserModel { Id = 1, FirstName = "Christoffer", LastName = "Testsson" };
        var user2 = new UserModel { Id = 2, FirstName = "Mirre", LastName = "Testsson" };
        userService.AddUser(user1);
        userService.AddUser(user2);

        //Act
        var allUsers = userService.GetAllUsers();

        //Assert
        Assert.Equal(2, allUsers.Count);
        Assert.Equal("Christoffer", allUsers[0].FirstName);
        Assert.Equal("Mirre", allUsers[1].FirstName);
    }

    [Fact]
    public void DeleteUser_ShouldRemoveUser_FromListAndFile() 
    { 
        //Arrange
        var mockFileService = new Mock<IUserFileService>();
        mockFileService.Setup(m => m.LoadFromFile()).Returns(new List<UserModel>());
        var userService = new UserService(mockFileService.Object);

        var user = new UserModel { Id = 1, FirstName = "Christoffer", LastName = "Testsson" };
        userService.AddUser(user);
        //Act
        userService.DeleteUser(user.Id);

        //Assert
        mockFileService.Verify(m => m.SaveToFile(It.IsAny<List<UserModel>>()), Times.AtLeastOnce);
        Assert.Empty(userService.GetAllUsers());
    }

    [Fact]
    public void UpdateUser_ShouldUpdateUserInformation_AndKeepInformationIfBlank() 
    { 
        //Arrange
        var mockFileService = new Mock<IUserFileService>();
        mockFileService.Setup(m => m.LoadFromFile()).Returns(new List<UserModel>());
        var userService = new UserService(mockFileService.Object);

        var user = new UserModel {
            Id = 1,
            FirstName = "Christoffer",
            LastName = "Testsson",
            Email = "",
            PhoneNumber = "1234567890",
            Adress = "Gatan 22",
            PostalCode = "12345",
            City = "Borås"
        };
        userService.AddUser(user);

        //Act
        user.Email = "ct@domain.com";
        user.City = "Mölndal";
        
        userService.UpdateUser(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.Adress, user.PostalCode, user.City );
    
        //Assert
        Assert.Equal("ct@domain.com", user.Email);
        Assert.Equal("Mölndal", user.City);
        Assert.Equal("Christoffer", user.FirstName);
        mockFileService.Verify(m => m.SaveToFile(It.IsAny<List<UserModel>>()), Times.AtLeastOnce);
    }

    [Fact]
    public void DeleteAllUsers_ShouldRemoveAllUsers_FromListAndFile() 
    { 
        //Arrange
        var mockFileService = new Mock<IUserFileService>();
        mockFileService.Setup(m => m.LoadFromFile()).Returns(new List<UserModel>());
        var userService = new UserService(mockFileService.Object);

        var user1 = new UserModel {FirstName = "Christoffer", LastName = "Testsson", };
        var user2 = new UserModel { FirstName = "Mirre", LastName = "Testsson", };
        userService.AddUser(user1);
        userService.AddUser(user2);

        //Act
        userService.DeleteAllUsers();

        //Assert
        Assert.Empty(userService.GetAllUsers());
        mockFileService.Verify(m => m.SaveToFile(It.IsAny<List<UserModel>>()), Times.AtLeastOnce);
    }

    [Fact]
    public void SaveUsersToFile_ShouldCallOnSaveToList_FromFileService() 
    {
        //Arrange
        var mockFileService = new Mock<IUserFileService>();
        mockFileService.Setup(m => m.LoadFromFile()).Returns(new List<UserModel>());
        var userService = new UserService(mockFileService.Object);

        var user1 = new UserModel { FirstName = "Christoffer", LastName = "Testsson", };
        var user2 = new UserModel { FirstName = "Mirre", LastName = "Testsson", };
        userService.AddUser(user1);
        userService.AddUser(user2);
        

        //Act
        userService.SaveUsersToFile();

        //Assert - Testar att SaveToFile anropats med rätt lista av användare
        mockFileService.Verify(m => m.SaveToFile(It.Is<List<UserModel>>(users =>
        users.Count == 2 &&
        users[0].FirstName == "Christoffer" &&
        users[0].LastName == "Testsson" &&
        users[1].FirstName == "Mirre" &&
        users[1].LastName == "Testsson")), Times.Exactly(3));

        //Metoden anropas 3 gånger. 2x i AddUser och en gång när SaveUsersToFile körs.

    }
}

    

