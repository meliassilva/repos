using Byui.ApiSnippet.Business.Business;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Sdk;

namespace Byui.ApiSnippet.Business.Test.Tests
{
    public class ExampleBusinessTest : IClassFixture<TemplateFixture>
    {
        private readonly ExampleBusiness _exampleBusiness;

        public ExampleBusinessTest(TemplateFixture fixture)
        {
            _exampleBusiness = fixture.ServiceProvider.GetService<ExampleBusiness>();
        }

        [Fact]
        public void GetValuesTest()
        {
            var values = _exampleBusiness.GetValues();

            Assert.NotNull(values);
            Assert.NotEmpty(values);
        }

        [Theory]
        [InlineData("value 1",true)]
        [InlineData("value 4", false)]
        public void GetValuesTest2(string value, bool contains)
        {
            var values = _exampleBusiness.GetValues();

            Assert.NotNull(values);
            Assert.Equal(contains,values.Contains(value));
        }

    }
}
