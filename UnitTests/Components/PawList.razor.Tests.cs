using Bunit;
using ContosoCrafts.WebSite.Components; // Adjust the namespace based on your project structure
using ContosoCrafts.WebSite.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests.Components
{
    public class PawListTests
    {
        [Test]
        public void PawList_Should_Return_Content()
        {
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);

            var page = context.RenderComponent<PawList>();

            var result = page.Markup;

            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Amy"));
            Assert.IsTrue(result.Contains("More Info"));
            Assert.IsTrue(result.Contains("Owner Info"));
        }

        [Test]
        public void SelectedPaw_Valid_Id_Should_Return_Content()
        {
            //Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var id = "moreInfo_7623900396";

            var page = context.RenderComponent<PawList>();

            var buttonList = page.FindAll("button");

            var button = buttonList.First(m => m.OuterHtml.Contains(id, StringComparison.OrdinalIgnoreCase));

            button.Click();

            var result = page.Markup;
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Hob is s an agile and intelligent dog with a striking black-and-white coat. He\u0027s a quick learner, excels in obedience training, and loves to show off his tricks."));
        }

        [Test]
        public void SelectedPaw_Owner_Info_Valid_Id_Should_Return_Content()
        {
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var id = "ownerInfo_7623900396";

            var page = context.RenderComponent<PawList>();

            var buttonList = page.FindAll("button");

            var button = buttonList.First(m => m.OuterHtml.Contains(id, StringComparison.OrdinalIgnoreCase));

            button.Click();

            var result = page.Markup;
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Michelle Brewer"));
        }

        [Test]
        public void SearchPaw_Valid_Name_Should_Return_Content()
        {
            //Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var id = "searchInput";
            var searchPawButtonId = "searchPaw";

            var page = context.RenderComponent<PawList>();

            var inputTags = page.FindAll("input");

            var input = inputTags.First(m => m.OuterHtml.Contains(id, StringComparison.OrdinalIgnoreCase));

            input.Change("Amy");

            var buttonList = page.FindAll("button");

            var button = buttonList.First(m => m.OuterHtml.Contains(searchPawButtonId, StringComparison.OrdinalIgnoreCase));

            button.Click();

            var result = page.Markup;
            Assert.NotNull(result);
            Assert.IsTrue(result.Contains("Amy"));
            Assert.IsFalse(result.Contains("Brooke"));
        }

        [Test]
        public void CLearText_Should_CLear_The_Search_Input_And_Return_Content()
        {
            //Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFilePawService>(TestHelper.PawService);
            var inputTextId = "searchInput";
            var searchPawButtonId = "searchPaw";
            var clearTextButtonId = "clearText";


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
            Assert.NotNull(updated_result);
            Assert.IsTrue(updated_result.Contains("Amy"));
            Assert.IsTrue(updated_result.Contains("Brooke"));
        }
    }
}
