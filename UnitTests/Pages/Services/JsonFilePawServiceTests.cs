using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.AddRating
{
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region UpdatePaw
        [Test]
        public void UpdatePaw_Invalid_Paw_Id_Should_Return_Null()
        {
            //Arrange
            var testpaw = new PawModel
            {
                Id = "542526163512",
                Paw = new Paw
                {
                    Name = "Paw",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = "Age",
                    Size = "Size",
                    Description = "Description",
                    Image = "Image"
                },
                Owner = new Owner
                {
                    Name = "Name",
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "paw@paw.com",
                    Phone = "Phone"
                }
            };

            //Act
            var result = TestHelper.PawService.UpdatePaw(testpaw);

            //Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void UpdatePaw_Valid_Paw_Id_Should_Return_True()
        {
            //Arrange
            var testpaw = new PawModel
            {
                Id = "5425261635",
                Paw = new Paw
                {
                    Name = "Paw",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = "Age",
                    Size = "Size",
                    Description = "Description",
                    Image = "Image"
                },
                Owner = new Owner
                {
                    Name = "Name",
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "paw@paw.com",
                    Phone = "Phone"
                }
            };

            //Act
            var result = TestHelper.PawService.UpdatePaw(testpaw);

            //Assert
            Assert.IsTrue(result);
        }
        #endregion UpdatePaw
    }
}