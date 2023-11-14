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
using System.Reflection;
using System.Runtime.InteropServices;

namespace UnitTests.Pages.Product.AddMeetup
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
        public static AddMeetupModel pageModel;


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

            var MockLoggerDirect = Mock.Of<ILogger<AddMeetupModel>>();
            JsonFilePawService pawService;

            pawService = new JsonFilePawService(mockWebHostEnvironment.Object);

            pageModel = new AddMeetupModel(pawService)
            {

            };
        }

        #endregion TestSetup

        
    }

}