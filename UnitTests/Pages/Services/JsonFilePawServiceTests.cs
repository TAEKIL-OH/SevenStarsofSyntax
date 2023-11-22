using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            var testpaw = new DetailedPawModel
            {
                Id = "542526163512",
                Paw = new PawModel
                {
                    Name = "Paw",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
            var InitialPaws = TestHelper.PawService.GetPaws();

            var testpaw = new DetailedPawModel
            {
                Id = "5425261635",
                Paw = new PawModel
                {
                    Name = "Amy Update Test",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
            var UpdatedPaw = TestHelper.PawService.GetPaws().FirstOrDefault(m => m.Id.Equals("5425261635"));

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Amy Update Test", UpdatedPaw.Paw.Name);

            //Reset
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
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
            var testpaw = new DetailedPawModel
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
            var testpaw = new DetailedPawModel
            {
                Id = "5425261635",
            };

            //Act
            var result = TestHelper.PawService.DeletePaw(testpaw.Id);
            var UpdatedPaws = TestHelper.PawService.GetPaws().FirstOrDefault(m => m.Id.Equals("5425261635"));


            //Assert
            Assert.IsTrue(result);
            Assert.AreNotEqual(InitialPaws, UpdatedPaws);

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
            var testpaw = new DetailedPawModel
            {
                Id = "542525163599",
                Paw = new PawModel
                {
                    Name = "Test",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
            var CreatedPaw = TestHelper.PawService.GetPaws().FirstOrDefault(m => m.Id.Equals("542525163599"));

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Test", CreatedPaw.Paw.Name);

            //Reset
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        #endregion CreatePaw

        #region SearchPaw

        [Test]

        /// <summary>
        /// Initialize an invalid paw name
        /// Test the paw data
        /// The result should be null
        /// </summary>
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

        /// <summary>
        /// Initialize an valid paw name
        /// Test the paw data
        /// The result should be paw data
        /// </summary>
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

        #region AddFeedbackToPaw
        [Test]

        /// <summary>
        /// Initialize an invalid paw id
        /// Test the paw data
        /// The result should be false
        /// </summary>
        public void AddFeedbackToPaw_Invalid_Paw_Id_Should_Return_False()
        {
            //Arrange
            var testpaw = new DetailedPawModel
            {
                Id = "542526163512",
            };
            var testMessage = "Great Paw";

            //Act
            bool result = TestHelper.PawService.AddFeedckToPaw(testpaw.Id, testMessage);

            //Result
            Assert.IsFalse(result);
        }

        [Test]

        /// <summary>
        /// Initialize an valid paw id with no feedback field
        /// Test the paw data
        /// The result should be true
        /// </summary>
        public void AddFeedbackToPaw_Valid_Paw_Id_With_No_Feedback_Should_Return_True()
        {
            //Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();
            var testpaw = new DetailedPawModel
            {
                Id = "5425261635",
                Paw = new PawModel
                {
                    Feedback = null
                }
            };
            var testMessage = "Great Paw";

            //Act
            bool result = TestHelper.PawService.AddFeedckToPaw(testpaw.Id, testMessage);
            var UpdatedPaw = TestHelper.PawService.GetPaws().FirstOrDefault(m => m.Id.Equals("5425261635"));

            //Result
            Assert.IsTrue(result);
            Assert.AreEqual("Great Paw", UpdatedPaw.Paw.Feedback[1]);

            //Reset
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Initialize an valid paw id
        /// Test the paw data
        /// The result should be true
        /// </summary>
        public void AddFeedbackToPaw_Valid_Paw_Id_With_Feedback_Should_Add_More_Feedback_And_Return_True()
        {
            //Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();
            var testpaw = new DetailedPawModel
            {
                Id = "7623900396",
            };
            var testMessage = "Great Paw There";

            //Act
            bool result = TestHelper.PawService.AddFeedckToPaw(testpaw.Id, testMessage);
            var UpdatedPaw = TestHelper.PawService.GetPaws().FirstOrDefault(m => m.Id.Equals("7623900396"));

            //Result
            Assert.IsTrue(result);
            Assert.AreEqual("Great Paw There", UpdatedPaw.Paw.Feedback[1]);

            //Reset
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        #endregion AddFeedbackToPaw

        #region AddMeetup

        [Test]

        /// <summary>
        /// Initialize an invalid paw id for the first paw
        /// Test the paw data
        /// The result should be false
        /// </summary>
        public void AddMeetup_Invalid_First_Paw_Id_Should_Return_False()
        {
            //Arrange
            string pawOne = "542526163587";
            string pawTwo = "7115673952";
            string dateOfMeetup = "11/13/2023";
            string location = "Seattle, WA";
            string message = "Nothing";


            //Act
            bool result = TestHelper.PawService.AddMeetup(pawOne, pawTwo, dateOfMeetup, location, message);

            //Result
            Assert.IsFalse(result);
        }

        [Test]

        /// <summary>
        /// Initialize an invalid paw id for the second paw
        /// Test the paw data
        /// The result should be false
        /// </summary>
        public void AddMeetup_Invalid_Second_Paw_Id_Should_Return_False()
        {
            //Arrange
            string pawOne = "5425261635";
            string pawTwo = "7115673952456";
            string dateOfMeetup = "11/13/2023";
            string location = "Seattle, WA";
            string message = "Nothing";

            //Act
            bool result = TestHelper.PawService.AddMeetup(pawOne, pawTwo, dateOfMeetup, location, message);

            //Result
            Assert.IsFalse(result);
        }

        [Test]

        /// <summary>
        /// Initialize the valid paw id
        /// Test the paw data for the paws who have no records for meetup
        /// The result should be true
        /// </summary>
        public void AddMeetup_Valid_First_Paw_Id_With_No_Meetup_Should_Return_True()
        {
            //Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            string pawOne = "9869756897";
            string pawTwo = "3593932834";
            string dateOfMeetup = "11/13/2023";
            string location = "Seattle, WA";
            string message = "Nothing";

            //Act
            bool result = TestHelper.PawService.AddMeetup(pawOne, pawTwo, dateOfMeetup, location, message);

            //Result
            Assert.IsTrue(result);

            //Reset
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Initialize the valid paw id
        /// Test the paw data for the second paw who have no records for meetup
        /// The result should be true
        /// </summary>
        public void AddMeetup_Valid_First_Second_Id_With_No_Meetup_Should_Return_True()
        {
            //Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            string pawOne = "7623900396";
            string pawTwo = "6225046605";
            string dateOfMeetup = "11/13/2023";
            string location = "Seattle, WA";
            string message = "Nothing";

            //Act
            bool result = TestHelper.PawService.AddMeetup(pawOne, pawTwo, dateOfMeetup, location, message);

            //Result
            Assert.IsTrue(result);

            //Reset
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Initialize the valid paw id
        /// Test the paw data for the paws who have alreadt records for meetup
        /// The result should be true
        /// </summary>
        public void AddMeetup_Valid_Paw_Id_With_Meetup_Should_Add_More_Meetups_And_Return_True()
        {
            //Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            string pawOne = "7623900396";
            string pawTwo = "3593932834";
            string dateOfMeetup = "11/13/2023";
            string location = "Seattle, WA";
            string message = "Nothing";

            //Act
            bool result = TestHelper.PawService.AddMeetup(pawOne, pawTwo, dateOfMeetup, location, message);

            //Result
            Assert.IsTrue(result);

            //Reset
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        #endregion AddMeetup
    }

}