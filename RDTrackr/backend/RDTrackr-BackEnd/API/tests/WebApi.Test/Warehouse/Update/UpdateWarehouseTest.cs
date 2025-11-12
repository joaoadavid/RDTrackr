using CommonTestUtilities.Requests;
using Shouldly;
using System.Net;
using WebApi.Test.InlineData;
using System.Text.Json;
using CommonTestUtilities.Requests.Warehouse;
using CommonTestUtilities.Tokens;

namespace WebApi.Test.Warehouse
{
    public class UpdateWarehouseTest : RDTrackRClassFixture
    {
        private readonly string _method = "warehouse";
        private readonly Guid _userIdentifier;
        public UpdateWarehouseTest(CustomWebApplicationFactory factory) : base(factory) 
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var register = RequestRegisterWarehouseJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var created = await DoPost(method: _method, request: register, token:token);

            var body = await created.Content.ReadAsStreamAsync();
            var data = await JsonDocument.ParseAsync(body);
            var id = data.RootElement.GetProperty("id").GetInt64();

            var update = RequestUpdateWarehouseJsonBuilder.Build();
            var response = await DoPut(method: $"{_method}/{id}", request: update);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Error_Not_Found()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var update = RequestUpdateWarehouseJsonBuilder.Build();
            var response = await DoPut(method: $"{_method}/9999", request: update,token:token);

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
