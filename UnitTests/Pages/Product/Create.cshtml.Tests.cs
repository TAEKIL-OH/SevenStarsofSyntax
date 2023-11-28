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
using System.Linq;

namespace UnitTests.Pages.Product.Create
{
    /// <summary>
    /// Unit testing for Creating a new paw data
    /// </summary>
    public class CreateTests
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
        public static CreateModel pageModel;


        [SetUp]
        /// <summary>
        /// Initializes mock Create page model for testing.
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

            var MockLoggerDirect = Mock.Of<ILogger<CreateModel>>();
            JsonFilePawService pawService;

            pawService = new JsonFilePawService(mockWebHostEnvironment.Object);

            pageModel = new CreateModel(pawService)
            {

            };
        }

        #endregion TestSetup

        #region OnPost
        /// <summary>
        /// Test case for invalid model state that should return a page
        /// </summary>
        [Test]
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

        /// <summary>
        /// Test case for null Id field value should return a page with invalid state
        /// </summary>
        [Test]
        public void OnPost_Invalid_Id_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new DetailedPawModel
            {
                Id = null,
                Paw = new PawModel
                {
                    Name = "Name",
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
                    Email = "",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Test case for empty Id field value should return a page with invalid state invalid state
        /// </summary>
        [Test]
        public void OnPost_Invalid_Id_Empty_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new DetailedPawModel
            {
                Id = "",
                Paw = new PawModel
                {
                    Name = "Name",
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
        /// Test case for null name field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Name_Null_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = null,
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = null,
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = "",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
        /// Test case for over limit description field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Description_Length_More_Than_Limit_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
                    Age = 1.0,
                    Size = "Size",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed euismod, quam a hendrerit cursus, nisl ligula ultricies velit, ut fermentum arcu nisi eu turpis. Phasellus vel purus vitae ex cursus fringilla nec sit amet urna. Proin at lacinia nulla. Vestibulum convallis sapien a tortor cursus, ut fermentum nisl luctus. Curabitur et leo vel justo vulputate cursus. Integer ut luctus dui. Sed at tortor vitae odio placerat imperdiet. Fusce non lacus ac nunc interdum efficitur. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc auctor, eros in bibendum tristique, sem sapien lacinia justo, vel interdum libero orci eu metus. Sed euismod tellus in justo dapibus, sit amet sodales nunc tristique. Nulla facilisi. Integer tristique justo eget enim tincidunt, vel imperdiet orci fermentum. Curabitur pharetra ligula id urna volutpat, eget commodo odio blandit. In hac habitasse platea dictumst.\r\n\r\n\r\n\r\n\r\n\r\n",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
                    Breed = "Breed",
                    Gender = (ContosoCrafts.WebSite.Enums.GenderTypeEnum)1,
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
        /// Test case for over limit owner address field value should return a page with invalid state
        /// </summary>
        public void OnPost_Invalid_Owner_Address_More_Than_Limit_Should_Return_Page()
        {
            // Arrange
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
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
                    Address = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed euismod, quam a hendrerit cursus, nisl ligula ultricies velit, ut fermentum arcu nisi eu turpis. Phasellus vel purus vitae ex cursus fringilla nec sit amet urna. Proin at lacinia nulla. Vestibulum convallis sapien a tortor cursus, ut fermentum nisl luctus. Curabitur et leo vel justo vulputate cursus. Integer ut luctus dui. Sed at tortor vitae odio placerat imperdiet. Fusce non lacus ac nunc interdum efficitur. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc auctor, eros in bibendum tristique, sem sapien lacinia justo, vel interdum libero orci eu metus. Sed euismod tellus in justo dapibus, sit amet sodales nunc tristique. Nulla facilisi. Integer tristique justo eget enim tincidunt, vel imperdiet orci fermentum. Curabitur pharetra ligula id urna volutpat, eget commodo odio blandit. In hac habitasse platea dictumst.\r\n\r\n\r\n\r\n\r\n\r\n",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
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
            pageModel.Paw = new DetailedPawModel
            {
                Id = "id",
                Paw = new PawModel
                {
                    Name = "Name",
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
                    Email = "Email",
                    Phone = ""
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.False(pageModel.ModelState.IsValid);
        }

        [Test]
        /// <summary>
        /// Test case for valid inputs should create paw and then reset the data after passing the case
        /// </summary>
        public void OnPost_Valid_Input_Should_Return_Page()
        {
            // Arrange
            var InitialPaws = pageModel.PawService.GetPaws();
            pageModel.Paw = new DetailedPawModel
            {
                Id = "1243658790",
                Paw = new PawModel
                {
                    Name = "Name Test Create",
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
                    Email = "Email",
                    Phone = "Phone"
                }

            };

            // act
            var result = pageModel.OnPost() as RedirectToPageResult;
            pageModel.PawService.GetPaws().FirstOrDefault(m => m.Id.Equals(1243658790));

            // Assert
            Assert.True(pageModel.ModelState.IsValid);
            Assert.AreEqual("Name Test Create", pageModel.Paw.Paw.Name);

            //Reset
            pageModel.PawService.SavePawsDataToJsonFile(InitialPaws);
        }
        #endregion OnPost

    }
}