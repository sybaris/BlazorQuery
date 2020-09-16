using BlazorQuery.Library;
using BlazorQuery.TestApp.CommonLib.Pages.UnitTests;
using Bunit;
using Bunit.TestDoubles.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlazorQuery.UnitTests
{
    public class UT_Text : TestContext
    {
        [Fact]
        public async Task Test1()
        {
            using var ctx = new TestContext();

            // Register services
            ctx.Services.AddTransient<BlazorQueryDOM>();
            ctx.Services.AddMockJSRuntime(JSRuntimeMockMode.Strict);

            // Arrange
            var cut = ctx.RenderComponent<Text>();
            cut.Find("h1").MarkupMatches("<h1 id=\"first_h1\">Hello, DOM!</h1>");

            // Act
            var element = cut.Find("button");
            await element.ClickAsync(null);

            //Assert
            cut.Find("h1").MarkupMatches("<h1 id=\"first_h1\">Now this text is changed</h1>");
        }
    }
}
