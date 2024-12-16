

using Business.Models;

namespace Business.tests.Models;

public class UserModel_Tests
{
    [Fact]
    public void UserModel_ShouldGetAndSetPropertiesCorrectly() 
    {
        //Arrange
        int id = 1;
        string firstName = "";
        string lastName = "";
        string email = "email@domain.com";
        string phoneNumber = "1234567890";
        string adress = "Gatan22";
        string postalCode = "12345";
        string city = "Göteborg";

        //Act
        UserModel result = new()
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Adress = adress,
            PostalCode = postalCode,
            City = city
        };


        //Assert - testar att UserModel hämtar och sätter värden korrekt.
        Assert.Equal( id, result.Id );
        Assert.Equal( firstName, result.FirstName );
        Assert.Equal( lastName, result.LastName );
        Assert.Equal( email, result.Email );
        Assert.Equal( phoneNumber, result.PhoneNumber );
        Assert.Equal ( adress, result.Adress );
        Assert.Equal( postalCode, result.PostalCode );
        Assert.Equal (city, result.City );
    } 

}
