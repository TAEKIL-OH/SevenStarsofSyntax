using Bunit;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests.Components
{
    /// <summary>
    /// Unit testing for components file
    /// </summary>
    public class PawListTests
    {
        [Test]

        /// <summary>
        /// Test case for displaying the paw lists
        /// </summary>
        public void PawList_Should_Return_Content()
        {
            //Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);

            //Act
            var page = context.RenderComponent<PawList>();
            var result = page.Markup;

            //Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Amy"));
            Assert.IsTrue(result.Contains("More Info"));
            Assert.IsTrue(result.Contains("Owner Info"));
        }

        [Test]

        /// <summary>
        /// Test case for the more info button
        /// </summary>
        public void SelectedPaw_Valid_Id_Should_Return_Content()
        {
            //Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var id = "moreInfo_7623900396";

            //Act
            var page = context.RenderComponent<PawList>();
            var buttonList = page.FindAll("button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id, StringComparison.OrdinalIgnoreCase));
            button.Click();
            var result = page.Markup;

            //Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Hob is s an agile and intelligent dog with a striking black-and-white coat. He\u0027s a quick learner, excels in obedience training, and loves to show off his tricks."));
        }

        [Test]

        /// <summary>
        /// Test case for owner info button
        /// </summary>
        public void SelectedPaw_Owner_Info_Valid_Id_Should_Return_Content()
        {
            //Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var id = "ownerInfo_7623900396";

            //Act
            var page = context.RenderComponent<PawList>();
            var buttonList = page.FindAll("button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id, StringComparison.OrdinalIgnoreCase));
            button.Click();
            var result = page.Markup;

            //Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Michelle Brewer"));
        }

        [Test]

        /// <summary>
        /// Test case for owner info button
        /// </summary>
        public void SelectedPaw_Meetup_Info_Valid_Id_Should_Return_Content()
        {
            //Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var id = "meetInfo_5425261635";

            //Act
            var page = context.RenderComponent<PawList>();
            var buttonList = page.FindAll("button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id, StringComparison.OrdinalIgnoreCase));
            button.Click();
            var result = page.Markup;

            //Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Brooke"));
        }

        [Test]

        /// <summary>
        /// Test case for searching valid paw value should return the paw
        /// </summary>
        public void SearchPaw_Valid_Name_Should_Return_Content()
        {
            //Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var id = "searchInput";
            var searchPawButtonId = "searchPaw";

            //Act
            var page = context.RenderComponent<PawList>();
            var inputTags = page.FindAll("input");
            var input = inputTags.First(m => m.OuterHtml.Contains(id, StringComparison.OrdinalIgnoreCase));
            input.Change("Amy");
            var buttonList = page.FindAll("button");
            var button = buttonList.First(m => m.OuterHtml.Contains(searchPawButtonId, StringComparison.OrdinalIgnoreCase));
            button.Click();
            var result = page.Markup;

            //Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Amy"));
            Assert.IsFalse(result.Contains("Brooke"));
        }

        [Test]

        /// <summary>
        /// Test case for clearing the text of searching paw and return the original list
        /// </summary>
        public void CLearText_Should_CLear_The_Search_Input_And_Return_Content()
        {
            //Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var inputTextId = "searchInput";
            var searchPawButtonId = "searchPaw";
            var clearTextButtonId = "clearText";

            //Act
            var page = context.RenderComponent<PawList>();
            var inputTags = page.FindAll("input");
            var input = inputTags.First(m => m.OuterHtml.Contains(inputTextId, StringComparison.OrdinalIgnoreCase));
            input.Change("Amy");
            var buttonList = page.FindAll("button");
            var searchpawbutton = buttonList.First(m => m.OuterHtml.Contains(searchPawButtonId, StringComparison.OrdinalIgnoreCase));
            searchpawbutton.Click();
            var result = page.Markup;
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Amy"));
            Assert.IsFalse(result.Contains("Brooke"));
            var clearpawbutton = buttonList.First(m => m.OuterHtml.Contains(clearTextButtonId, StringComparison.OrdinalIgnoreCase));
            clearpawbutton.Click();
            var updated_result = page.Markup;

            //Assert
            Assert.NotNull(updated_result);
            Assert.IsTrue(updated_result.Contains("Amy"));
            Assert.IsTrue(updated_result.Contains("Brooke"));
        }

        [Test]

        /// <summary>
        /// Test case for adding new feedback in the homepage
        /// </summary>
        public void AddFeedack_Should_Add_The_Feedback_From_Input_And_Close_Modal()
        {
            //Arrange
            var InitialPaws = TestHelper.PawService.GetPaws();
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var id = "moreInfo_7623900396";
            var feedbackMessageId = "feedbackMessage";
            var addFeedbackId = "addFeedback";

            //Act
            var page = context.RenderComponent<PawList>();
            var buttonList = page.FindAll("button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id, StringComparison.OrdinalIgnoreCase));
            button.Click();

            var buttonMarkup = page.Markup;

            var inputTags = page.FindAll("input");
            var message = inputTags.First(i => i.OuterHtml.Contains(feedbackMessageId, StringComparison.OrdinalIgnoreCase));
            message.Change("amazing Hob so great");

            var inputMarkup = page.Markup;

            var feedbackbtnlist = page.FindAll("button");
            var addFeedbackbutton = feedbackbtnlist.First(b => b.OuterHtml.Contains(addFeedbackId, StringComparison.OrdinalIgnoreCase));
            addFeedbackbutton.Click();

            var secondTimeButtonMarkup = page.Markup;

            var initialButtonLists = page.FindAll("button");
            var moreInfoButton = initialButtonLists.First(m => m.OuterHtml.Contains(id, StringComparison.OrdinalIgnoreCase));
            moreInfoButton.Click();

            var result = page.Markup;

            //Assert
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("amazing Hob so great"));

            //Reset
            TestHelper.PawService.SavePawsDataToJsonFile(InitialPaws);
        }

    }

}