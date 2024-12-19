

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
    

}
