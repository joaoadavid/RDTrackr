using CommonTestUtilities.Requests;
using CommonTestUtilities.Requests.Warehouse;
using CommonTestUtilities.Tokens;
using RDTrackR.Exceptions;
using Shouldly;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Warehouse.Register
{
    public class RegisterWarehouseTest : RDTrackRClassFixture
    {
        private readonly string _method = "warehouse";
        private readonly Guid _userIdentifier;
        public RegisterWarehouseTest(CustomWebApplicationFactory factory) : base(factory) 
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {

            var request = RequestRegisterWarehouseJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var response = await DoPost(method: _method, request: request, token:token);

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            await using var body = await response.Content.ReadAsStreamAsync();
            var data = await JsonDocument.ParseAsync(body);

            data.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Name(string culture)
        {
            var request = RequestRegisterWarehouseJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            request.Name = string.Empty;

            var response = await DoPost(method: _method, request: request, token:token, culture: culture);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            await using var body = await response.Content.ReadAsStreamAsync();
            var data = await JsonDocument.ParseAsync(body);

            var errors = data.RootElement.GetProperty("errors").EnumerateArray();
            var expectedMessage = ResourceMessagesException.ResourceManager
                .GetString("WAREHOUSE_NAME_REQUIRED", new System.Globalization.CultureInfo(culture));

            errors.ShouldContain(e => e.GetString() == expectedMessage);
        }
    }
}
