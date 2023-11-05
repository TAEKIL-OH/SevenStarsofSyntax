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
        /// <summary>
        /// REST Initialize an invalid paw id
        /// Test the invalid paw data
        /// The result should be false
        /// </summary>
        [Test]
        public void UpdatePaw_Invalid_Paw_Id_Should_Return_False()
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
            var result = TestHelper.PawService.UpdatePaw(testpaw);

            //Assert
            Assert.IsFalse(result);
        }
        /// <summary>
        /// REST Initialize an valid paw id
        /// Test the valid paw data
        /// The result should be true
        /// </summary>
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
                    Age = 1.1,
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
        /// <summary>
        /// REST Initialize an invalid paw id
        /// Test the invalid paw data
        /// The result should be false
        /// </summary>
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
        /// <summary>
        /// REST Initialize an valid paw id
        /// Test the valid paw data
        /// The result should be true
        /// After the test the json file is to be reset with original data
        /// </summary>
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
        /// <summary>
        /// REST Initialize an paw data
        /// Test the paw data
        /// The result should be true
        /// </summary>
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

        #region SearchPaw
        [Test]
        public void SearchPaw_InValid_Paw_Name_Should_Return_Null()
        {
            //Arrange
            var pawToSearch = "Team7";

            //Act
            var result = TestHelper.PawService.SearchPaw(pawToSearch);

            //Assert
            Assert.IsNull(result);
        }
        [Test]
        public void SearchPaw_Valid_Paw_Name_Should_Return_Paw()
        {
            //Arrange
            var pawToSearch = "Amy";

            //Act
            var result = TestHelper.PawService.SearchPaw(pawToSearch);

            //Assert
            Assert.IsTrue(result.All(paw => paw.Paw.Name.Equals(pawToSearch)));
        }
        #endregion SearchPaw


    }
}