using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;
using System.Runtime.InteropServices;

namespace UnitTests.Pages.Service.JsonFilePawService
{
    public class JsonFilePawServiceTests
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

        #region DeletePaw
        [Test]
        public void DeletePaw_Invalid_Paw_Id_Should_Return_False()
        {
            //Arrange
            var testpaw = new PawModel
            {
                Id = "542526163512",
            };

            //Act
            var result = TestHelper.PawService.DeletePaw(testpaw.Id);

            //Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void DeletePaw_Invalid_Paw_Id_Should_Return_True()
        {
            //Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();
            var testpaw = new PawModel
            {
                Id = "5425261635",
            };

            //Act
            var result = TestHelper.PawService.DeletePaw(testpaw.Id);

            //Assert
            Assert.IsTrue(result);

            //Reset
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }
        #endregion DeletePaw

        #region CreatePaw

        [Test]

        public void CreatePaw_Valid_Paw_Detail_Should_Return_True()

        {

            //Arrange

            var InitialPaws = TestHelper.PawService.GetPaws();

            var testpaw = new PawModel

            {

                Id = "5425251635",

                Paw = new Paw

                {

                    Name = "Test",

                    Breed = "Breed",

                    Gender = "Gender",

                    Age = 1.0,

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

            var result = TestHelper.PawService.CreatePaw(testpaw);

            //Assert

            Assert.IsTrue(result);

            //Reset

            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);

        }

        #endregion CreatePaw


    }
}