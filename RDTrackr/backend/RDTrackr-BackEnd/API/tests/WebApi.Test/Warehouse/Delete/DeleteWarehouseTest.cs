using CommonTestUtilities.Entities;
using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Requests.Warehouse;
using CommonTestUtilities.Tokens;
using RDTrackR.Exceptions;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Warehouse.Delete
{
    public class DeleteWarehouseTest : RDTrackRClassFixture
    {
        private readonly string METHOD = "warehouse";
        private readonly RDTrackR.Domain.Entities.User _userIdentifier;
        private readonly long _warehouseId;

        public DeleteWarehouseTest(CustomWebApplicationFactory factory) : base(factory) 
        {
            _userIdentifier = factory.GetUser();
            _warehouseId = factory.GetWarehouseId();
        }

        [Fact]
        public async Task Success()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoDelete(method: $"{METHOD}/{_warehouseId}", token:token);

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            response = await DoGet($"{METHOD}/{_warehouseId}", token);

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Not_Found(string culture)
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            var response = await DoDelete(method: $"{METHOD}/9999",token:token, culture:culture);

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
