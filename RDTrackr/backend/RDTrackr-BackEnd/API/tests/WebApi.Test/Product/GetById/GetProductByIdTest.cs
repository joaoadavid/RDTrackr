using CommonTestUtilities.Tokens;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Product.GetById
{
    public class GetProductByIdTest : RDTrackRClassFixture
    {
        private const string METHOD = "product";
        private readonly Guid _userIdentifier;
        private readonly long _productId;
        private readonly string _productName;

        public GetProductByIdTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _productId = factory.GetProductId();
            _productName = factory.GetProductName();
        }

        [Fact]
        public async Task Success()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoGet($"{METHOD}/{_productId}", token);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().ShouldBe(_productName);
        }
    }
}

