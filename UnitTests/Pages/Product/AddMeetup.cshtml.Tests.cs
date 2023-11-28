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
using System.Linq;

namespace UnitTests.Pages.Product.AddMeetup
{
    /// <summary>
    /// Unit testing for adding a new meetup in paw data
    /// </summary>
    public class AddMeetup
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
        public static AddMeetupModel pageModel;


        [SetUp]

        /// <summary>
        /// Initializes mock AddMeetupModel page model for testing.
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

            var MockLoggerDirect = Mock.Of<ILogger<AddMeetupModel>>();
            JsonFilePawService pawService;

            pawService = new JsonFilePawService(mockWebHostEnvironment.Object);

            pageModel = new AddMeetupModel(pawService)
            {
                TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
            };
        }

        #endregion TestSetup

        #region OnGet

        [Test]

        /// <summary>
        /// Test case for requesting valid paws value should return the paws
        /// </summary>
        public void OnGet_Valid_Should_Return_Requested_Paw()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(20, pageModel.Paw.ToList().Count);
        }

        #endregion OnGet

        #region  OnPost

        [Test]

        /// <summary>
        /// Test case for invalid model state should return the page
        /// </summary>
        public void OnPost_InValid_Model_State_Should_Return_Page()
        {
            // Arrange
            pageModel.pawOne = "7623900396";
            pageModel.pawTwo = "3593932834";
            pageModel.meetupDate = "11/13/2023";
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Nothing";
            pageModel.ModelState.AddModelError("ModelOnly", "Something went wrong");

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("ModelOnly"));
        }

        [Test]

        /// <summary>
        /// Test case for invalid paw one null state should return the page
        /// </summary>
        public void OnPost_InValid_Paw_One_Null_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = null;
            pageModel.pawTwo = "3593932834";
            pageModel.meetupDate = "11/13/2023";
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("pawOne"));

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for invalid paw one empty should return the page
        /// </summary>
        public void OnPost_InValid_Paw_One_Empty_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "";
            pageModel.pawTwo = "3593932834";
            pageModel.meetupDate = "11/13/2023";
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("pawOne"));

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for invalid paw two null should return the page
        /// </summary>
        public void OnPost_InValid_Paw_Two_Null_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "7623900396";
            pageModel.pawTwo = null;
            pageModel.meetupDate = "11/13/2023";
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("pawTwo"));

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for invalid paw two empty should return the page
        /// </summary>
        public void OnPost_InValid_Paw_Two_Empty_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "7623900396";
            pageModel.pawTwo = "";
            pageModel.meetupDate = "11/13/2023";
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("pawTwo"));

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for invalid meetup date null should return the page
        /// </summary>
        public void OnPost_InValid_Meetup_Date_Null_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "7623900396";
            pageModel.pawTwo = "7623900196";
            pageModel.meetupDate = null;
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("meetupDate"));

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for invalid meetup date empty should return the page
        /// </summary>
        public void OnPost_InValid_Meetup_Date_Empty_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "7623900396";
            pageModel.pawTwo = "7623900196";
            pageModel.meetupDate = "";
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("meetupDate"));

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for invalid meetup locatiom null should return the page
        /// </summary>
        public void OnPost_InValid_Meetup_Location_Null_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "7623900396";
            pageModel.pawTwo = "7623900196";
            pageModel.meetupDate = "11/28/2023";
            pageModel.meetupLocation = null;
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("meetupLocation"));

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for invalid meetup location empty should return the page
        /// </summary>
        public void OnPost_InValid_Meetup_Location_Empty_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "7623900396";
            pageModel.pawTwo = "7623900196";
            pageModel.meetupDate = "11/28/2023";
            pageModel.meetupLocation = "";
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("meetupLocation"));

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for invalid message over limit empty should return the page
        /// </summary>
        public void OnPost_InValid_Message_Over_Limit_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "7623900396";
            pageModel.pawTwo = "7623900196";
            pageModel.meetupDate = "11/28/2023";
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed euismod, quam a hendrerit cursus, nisl ligula ultricies velit, ut fermentum arcu nisi eu turpis. Phasellus vel purus vitae ex cursus fringilla nec sit amet urna. Proin at lacinia nulla. Vestibulum convallis sapien a tortor cursus, ut fermentum nisl luctus. Curabitur et leo vel justo vulputate cursus. Integer ut luctus dui. Sed at tortor vitae odio placerat imperdiet. Fusce non lacus ac nunc interdum efficitur. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc auctor, eros in bibendum tristique, sem sapien lacinia justo, vel interdum libero orci eu metus. Sed euismod tellus in justo dapibus, sit amet sodales nunc tristique. Nulla facilisi. Integer tristique justo eget enim tincidunt, vel imperdiet orci fermentum. Curabitur pharetra ligula id urna volutpat, eget commodo odio blandit. In hac habitasse platea dictumst.\r\n\r\n\r\n\r\n\r\n\r\n";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(pageModel.ModelState.IsValid);
            Assert.IsTrue(pageModel.ModelState.ContainsKey("message"));

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for invalid paw id should return the page
        /// </summary>
        public void OnPost_InValid_Paw_Id_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "Team7";
            pageModel.pawTwo = "7Team";
            pageModel.meetupDate = "11/13/2023";
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message) as RedirectToPageResult;

            // Assert
            Assert.AreEqual("../Error", result.PageName);
            Assert.AreEqual("Something went wrong while adding meetup to the paw(s) please retry", pageModel.TempData["ErrorMessage"]);

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        [Test]

        /// <summary>
        /// Test case for valid data should add meetup and return the page
        /// </summary>
        public void OnPost_Valid_Data_Should_AddMeetup_And_Return_Page()
        {
            // Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();

            pageModel.pawOne = "7623900396";
            pageModel.pawTwo = "3593932834";
            pageModel.meetupDate = "11/13/2023";
            pageModel.meetupLocation = "Seattle, WA";
            pageModel.message = "Nothing";

            // Act
            var result = pageModel.OnPost(pageModel.pawOne, pageModel.pawTwo, pageModel.meetupDate, pageModel.meetupLocation, pageModel.message);

            // Assert
            Assert.IsTrue(pageModel.ModelState.IsValid);

            //Reset 
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

        #endregion OnPost

    }

}