using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.Delete
{
    /// <summary>
    /// Unit testing for Deleting a existing paw data
    /// </summary>
    public class DeleteTests
    {
        #region TestSetup
        /// <summary>
        /// Variales to be used while testing
        /// </summary>
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static DeleteModel pageModel;

        [SetUp]
        /// <summary>
        /// Initializes mock Delete page model for testing.
        /// </summary>
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<DeleteModel>>();
            JsonFilePawService pawService;

            pawService = new JsonFilePawService(mockWebHostEnvironment.Object);

            pageModel = new DeleteModel(pawService)
            {
                TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
            };
        }

        #endregion TestSetup

        #region OnGet
        [Test]
        /// <summary>
        /// Test case for requesting valid paw value should return the paw
        /// </summary>
        public void OnGet_Valid_Should_Return_Requested_Paw()
        {
            // Arrange

            // Act
            pageModel.OnGet("5425261635");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("5425261635", pageModel.Paw.Id);
        }

        [Test]

        /// <summary>
        /// Test case for requesting invalid paw value should return the error page
        /// </summary>
        public void OnGet_Invalid_Should_Set_TempData_And_Redirect_To_Error_Page()
        {
            // Arrange

            // Act
            var result = pageModel.OnGet("5425261635123") as RedirectToPageResult;

            // Assert
            Assert.AreEqual("../Error", result.PageName);
            Assert.AreEqual("Something went wrong while fetching the paw please retry", pageModel.TempData["ErrorMessage"]);
        }
        #endregion OnGet

        #region OnPost
        [Test]
        /// <summary>
        /// Test case for  invalid model state should return the page 
        /// </summary>
        public void OnPost_InValid_Model_State_Should_Return_Page()
        {
            // Arrange
            pageModel.ModelState.AddModelError("ModelOnly", "Something went wrong");

            // Act
            var result = pageModel.OnPost();

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);

            // Check for the specific error message in the model state.

            Assert.IsTrue(pageModel.ModelState.ContainsKey("ModelOnly"));
        }

        [Test]
        /// <summary>
        /// Test case for delete invalid paw value should return the page 
        /// </summary>
        public void OnPost_InValid_Paw_Data_Should_Return_Page()
        {
            pageModel.Paw = new DetailedPawModel
            {
                Id = "542526163512",
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual("../Error", result.PageName);
            Assert.AreEqual("Something went wrong while deleting the paw please retry", pageModel.TempData["ErrorMessage"]);
        }

        [Test]
        /// <summary>
        /// Test case for delete valid paw value should delete the paw then check if the paw exists after the deletion and then reset the paw data after passing the test case 
        /// </summary>
        public void OnPost_Valid_Paw_Data_Should_Return_Page()
        {
            var InitialPaws = pageModel.PawService.GetPaws();
            pageModel.Paw = new DetailedPawModel
            {
                Id = "5425261635",
            };

            // Act
            var result = pageModel.OnPost() as PageResult;

            //Checking if the paw dosent exist after deletion
            var UpdatedPaws = pageModel.PawService.GetPaws();

            //Proved that deletion operation worked
            Assert.AreNotEqual(InitialPaws, UpdatedPaws);

            // Assert
            Assert.True(pageModel.ModelState.IsValid);

            //Reset
            pageModel.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        #endregion OnPost

    }

}