using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Controllers
{
    /// <summary>
    /// Unit Testing for PawsController class
    /// </summary>
    public class PawsControllerTests
    {

        //Creating and instance
        public static PawsController pawController;

        /// <summary>
        /// Initializes paw Controller class for testing.
        /// </summary>
        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
            pawController = new PawsController(TestHelper.PawService);
        }
        #endregion TestSetup

        #region Get

        /// <summary>
        /// Testing if get is valid should return paws list
        /// </summary>
        [Test]
        public void Get_Valid_Should_Return_List_Of_Paws()
        {
            //Arrange

            //Act
            var data = pawController.Get().ToList();

            //Assert
            Assert.AreEqual(typeof(List<DetailedPawModel>), data.GetType());
        }

        /// <summary>
        /// Testing get valid tostring should return string
        /// </summary>
        [Test]
        public void Get_Valid_ToString_Should_Return_String()
        {
            {
                //Arrange

                //Act
                var data = pawController.Get().FirstOrDefault().ToString();

                //Assert
                Assert.AreEqual(typeof(string), data.GetType());
            }
        }

        #endregion Get
    }

}