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


namespace UnitTests.Pages.Product.AddFeedback
{
    /// <summary>
    /// Unit testing for adding a new feedack in paw data
    /// </summary>
    public class AddFeedback
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
        public static AddFeedbackModel pageModel;


        [SetUp]
        /// <summary>
        /// Initializes mock AddFeedbackModel page model for testing.
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

            var MockLoggerDirect = Mock.Of<ILogger<AddFeedbackModel>>();
            JsonFilePawService pawService;

            pawService = new JsonFilePawService(mockWebHostEnvironment.Object);

            pageModel = new AddFeedbackModel(pawService)
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
        /// Test case for invalid model state should return the page
        /// </summary>
        public void OnPost_InValid_Model_State_Should_Return_Page()
        {
            // Arrange
            pageModel.ModelState.AddModelError("ModelOnly", "Something went wrong");
            pageModel.message = "Good Paw";
            pageModel.Paw = new DetailedPawModel
            {
                Id = "5425261635"
            };

            // Act
            var result = pageModel.OnPost(pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);

            // Check for the specific error message in the model state.

            Assert.IsTrue(pageModel.ModelState.ContainsKey("ModelOnly"));
        }

        [Test]

        /// <summary>
        /// Test case for null message should return the page
        /// </summary>
        public void OnPost_Invalid_Message_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new DetailedPawModel
            {
                Id = "5425261635"
            };
            pageModel.message = null;

            // act
            var result = pageModel.OnPost(pageModel.message) as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]

        /// <summary>
        /// Test case for empty message should return the page
        /// </summary>
        public void OnPost_Invalid_Message_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new DetailedPawModel
            {
                Id = "5425261635"
            };
            pageModel.message = "";

            // act
            var result = pageModel.OnPost(pageModel.message) as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]

        /// <summary>
        /// Test case for over limit message should return the page
        /// </summary>
        public void OnPost_Invalid_Message_Over_Limit_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new DetailedPawModel
            {
                Id = "5425261635"
            };
            pageModel.message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed euismod, quam a hendrerit cursus, nisl ligula ultricies velit, ut fermentum arcu nisi eu turpis. Phasellus vel purus vitae ex cursus fringilla nec sit amet urna. Proin at lacinia nulla. Vestibulum convallis sapien a tortor cursus, ut fermentum nisl luctus. Curabitur et leo vel justo vulputate cursus. Integer ut luctus dui. Sed at tortor vitae odio placerat imperdiet. Fusce non lacus ac nunc interdum efficitur. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc auctor, eros in bibendum tristique, sem sapien lacinia justo, vel interdum libero orci eu metus. Sed euismod tellus in justo dapibus, sit amet sodales nunc tristique. Nulla facilisi. Integer tristique justo eget enim tincidunt, vel imperdiet orci fermentum. Curabitur pharetra ligula id urna volutpat, eget commodo odio blandit. In hac habitasse platea dictumst.\r\n\r\n\r\n\r\n\r\n\r\n";

            // act
            var result = pageModel.OnPost(pageModel.message) as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]

        /// <summary>
        /// Test case for invalid paw id should return the page
        /// </summary>
        public void OnPost_Invalid_Paw_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new DetailedPawModel
            {
                Id = "542526163534"
            };
            pageModel.message = "Test";

            // act
            var result = pageModel.OnPost(pageModel.message) as RedirectToPageResult;

            // Assert
            Assert.AreEqual("../Error", result.PageName);
            Assert.AreEqual("Something went wrong while adding feedack to the paw please retry", pageModel.TempData["ErrorMessage"]);
        }

        [Test]

        /// <summary>
        /// Test case for valid message should add feedack return to the page and reset the data
        /// </summary>
        public void OnPost_Valid_Message_No_Feedback_Should_Add_Feedack_And_Redirect_To_Page()
        {
            // Arrange
            var InitialPaws = pageModel.PawService.GetPaws();
            pageModel.Paw = new DetailedPawModel
            {
                Id = "7115673952"
            };
            pageModel.message = "Hey Paw";

            // act
            var result = pageModel.OnPost(pageModel.message) as RedirectToPageResult;
            pageModel.OnGet("7115673952");

            // Assert
            Assert.True(pageModel.ModelState.IsValid);
            Assert.AreEqual("Hey Paw", pageModel.Paw.Paw.Feedback[0]);

            //Reset
            pageModel.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for valid message should add feedack return to the page and reset the data
        /// </summary>
        public void OnPost_Valid_Message_With_Feedback_Should_Append_More_Feedack_And_Redirect_To_Page()
        {
            // Arrange
            var InitialPaws = pageModel.PawService.GetPaws();
            pageModel.Paw = new DetailedPawModel
            {
                Id = "5425261635"
            };
            pageModel.message = "Hey Paw";

            // act
            var result = pageModel.OnPost(pageModel.message) as RedirectToPageResult;
            pageModel.OnGet("5425261635");

            // Assert
            Assert.True(pageModel.ModelState.IsValid);
            Assert.AreEqual("Hey Paw", pageModel.Paw.Paw.Feedback[1]);

            //Reset
            pageModel.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        #endregion OnPost
    }

}