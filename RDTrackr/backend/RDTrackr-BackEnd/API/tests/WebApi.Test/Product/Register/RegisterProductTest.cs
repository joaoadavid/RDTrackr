using CommonTestUtilities.Requests;
using CommonTestUtilities.Requests.Supplier;
using CommonTestUtilities.Tokens;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Product.Register
{
    public class RegisterProductTest : RDTrackRClassFixture
    {
        private const string METHOD = "product";
        private readonly Guid _userIdentifier;

        public RegisterProductTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestProductJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var response = await DoPost(method: METHOD, request: request, token: token);

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            using var body = await response.Content.ReadAsStreamAsync();
            var data = await JsonDocument.ParseAsync(body);

            data.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        }
    }
}
