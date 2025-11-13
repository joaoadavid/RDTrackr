using CommonTestUtilities.Entities;
using CommonTestUtilities.Requests.Supplier;
using CommonTestUtilities.Tokens;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Supplier.Register
{
    public class RegisterSupplierTest : RDTrackRClassFixture
    {
        private const string endpoint = "supplier";
        private readonly RDTrackR.Domain.Entities.User _userIdentifier;

        public RegisterSupplierTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUser();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterSupplierJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var response = await DoPost(method:endpoint, request:request, token:token);

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            using var body = await response.Content.ReadAsStreamAsync();
            var data = await JsonDocument.ParseAsync(body);

            data.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        }
    }
}
