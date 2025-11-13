using CommonTestUtilities.Entities;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using RDTrackR.Exceptions;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Product.Update
{
    public class UpdateProductTest : RDTrackRClassFixture
    {
        private const string METHOD = "product";

        private readonly RDTrackR.Domain.Entities.User _userIdentifier;
        private readonly long _productId;

        public UpdateProductTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUser();
            _productId = factory.GetProductId();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestProductJsonBuilder.Build();

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPut($"{METHOD}/{_productId}", request, token);

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Title_Empty(string culture)
        {
            var request = RequestProductJsonBuilder.Build();
            request.Sku = string.Empty;

            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPut(method: $"{METHOD}/{_productId}", request: request, token: token, culture: culture);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("PRODUCT_SKU_REQUIRED", new CultureInfo(culture));

            errors.ShouldHaveSingleItem();
            errors.ShouldContain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
