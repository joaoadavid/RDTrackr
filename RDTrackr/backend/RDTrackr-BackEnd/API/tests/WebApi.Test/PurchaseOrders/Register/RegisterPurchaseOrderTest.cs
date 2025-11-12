using CommonTestUtilities.Requests;
using CommonTestUtilities.Requests.PurchaseOrder;
using CommonTestUtilities.Tokens;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.PurchaseOrders.Register
{
    public class RegisterPurchaseOrderTest : RDTrackRClassFixture
    {
        private const string METHOD = "purchaseorder";
        private readonly Guid _userIdentifier;

        public RegisterPurchaseOrderTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestPurchaseOrderJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPost(method: METHOD, request: request, token: token);

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            using var body = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(body);

            var json = responseData.RootElement;

            json.GetProperty("id").GetInt64().ShouldBeGreaterThan(0);
            json.GetProperty("status").GetString().ShouldBe("DRAFT");

            var returnedItems = json.GetProperty("items").EnumerateArray().ToList();
            returnedItems.Count.ShouldBe(request.Items.Count);

            for (int i = 0; i < request.Items.Count; i++)
            {
                returnedItems[i].GetProperty("quantity").GetDecimal()
                    .ShouldBe(request.Items[i].Quantity);

                returnedItems[i].GetProperty("unitPrice").GetDecimal()
                    .ShouldBe(request.Items[i].UnitPrice);
            }
        }
    }
}
