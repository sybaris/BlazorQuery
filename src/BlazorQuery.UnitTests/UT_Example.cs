using BlazorQuery.TestApp.CommonLib.Pages;
using System;
using Xunit;
using Bunit;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlazorQuery.UnitTests
{
    // https://www.telerik.com/blogs/unit-testing-blazor-components-bunit-justmock
    public class UT_Example : TestContext
    {
        [Fact]
        public void Test1()
        {
            using var ctx = new TestContext();
            // Arrange
            var cut = ctx.RenderComponent<Counter>();
            cut.Find("p").MarkupMatches("<p>Current count: 0</p>");

            // Act
            var element = cut.Find("button");
            element.Click();

            //Assert
            cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
        }
    }
}
