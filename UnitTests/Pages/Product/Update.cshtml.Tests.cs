
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

namespace UnitTests.Pages.Product.Update
{
    /// <summary>
    /// Unit testing for Updating a existing paw data
    /// </summary>
    public class UpdateTests
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

        public static UpdateModel pageModel;

        [SetUp]
        /// <summary>
        /// Initializes mock Update page model for testing.
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

            var MockLoggerDirect = Mock.Of<ILogger<UpdateModel>>();
            JsonFilePawService pawService;

            pawService = new JsonFilePawService(mockWebHostEnvironment.Object);

            pageModel = new UpdateModel(pawService)
            {

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
            Assert.AreEqual("Amy", pageModel.Paw.Paw.Name);
        }
        #endregion OnGet

        #region OnPost
        [Test]
        /// <summary>
        /// Test case for null name field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Name_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = null,
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
                    Email = "",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty name field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Name_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "",
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
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }
        /// <summary>
        /// Test case for null breed field value should return a page with invalid state
        /// </summary>
        [Test]
        public void OnPost_Invalid_Breed_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = null,
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
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Test case for empty breed field value should return a page with invalid state
        /// </summary>
        [Test]
        public void OnPost_InValid_Breed_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "",
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
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for invalid age field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Age_Less_Than_One_Month_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 0,
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
                    Email = "Email",
                    Phone = "Phone"
                }
            };
            // act
            var result = pageModel.OnPost() as RedirectToPageResult;
            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for null size field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Size_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 1.0,
                    Size = null,
                    Description = "Description",
                    Image = "Image"
                },
                Owner = new Owner
                {
                    Name = "Name",
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty size field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Size_Empty_Should_Return_Page()
        {
            // Arrange            
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 1.0,
                    Size = "",
                    Description = "Description",
                    Image = "Image"
                },
                Owner = new Owner
                {
                    Name = "Name",
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for null description field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Description_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 1.0,
                    Size = "Size",
                    Description = null,
                    Image = "Image"
                },
                Owner = new Owner
                {
                    Name = "Name",
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty description field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Description_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 1.0,
                    Size = "Size",
                    Description = "",
                    Image = "Image"
                },
                Owner = new Owner
                {
                    Name = "Name",
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for null image field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Image_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 1.0,
                    Size = "Size",
                    Description = "Description",
                    Image = null
                },
                Owner = new Owner
                {
                    Name = "Name",
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty image field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Image_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 1.0,
                    Size = "Size",
                    Description = "Description",
                    Image = ""
                },
                Owner = new Owner
                {
                    Name = "Name",
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for null owner name field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Name_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 1.0,
                    Size = "Size",
                    Description = "Description",
                    Image = "Image"
                },
                Owner = new Owner
                {
                    Name = null,
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty owner name field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Name_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 1.0,
                    Size = "Size",
                    Description = "Description",
                    Image = "Image"
                },
                Owner = new Owner
                {
                    Name = "",
                    Address = "Address",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for null owner address field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Address_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    Address = null,
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty owner address field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Address_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    Address = "",
                    City = "City",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for null owner city field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_City_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    City = null,
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty owner city field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_City_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    City = "",
                    Zipcode = "Zipcode",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for null owner zipcode field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Zipcode_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    Zipcode = null,
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty owner zipcode field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Zipcode_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    Zipcode = "",
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for null owner email field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Email_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    Zipcode = "zipcode",
                    Email = null,
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty owner email field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Email_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    Email = "",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for null owner phone field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Phone_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    Zipcode = "zipcode",
                    Email = "Email",
                    Phone = null
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for empty owner phone field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Phone_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
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
                    Email = "Email",
                    Phone = ""
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }
        /// <summary>
        /// Test case for inputting all valid input should update the paw and reset the data after passing the test case
        /// </summary>
        [Test]
        public void OnPost_Valid_Input_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = pageModel.PawService.GetPaws();

            pageModel.Paw = new PawModel
            {
                Id = "id",
                Paw = new Paw
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = "Gender",
                    Age = 1,
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
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.True(pageModel.ModelState.IsValid);

            //Reset
            pageModel.PawService.SavePawsDataToJsonFile(InitialPaws);

        }
        #endregion OnPost
    }
}