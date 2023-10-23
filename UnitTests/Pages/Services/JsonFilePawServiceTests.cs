using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.UpdatePaw
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
        public void UpdatePaw_Invalid_Paw_Null_Should_Return_Null()
        {
            //Arrange
            var testpaw = new PawModel();

            //Act
            var result = TestHelper.PawService.UpdatePaw(testpaw);

            //Assert
            Assert.AreEqual(testpaw, result);
        }
        #endregion UpdatePaw

    }
}