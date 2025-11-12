using CommonTestUtilities.Requests;
using CommonTestUtilities.Requests.PurchaseOrder;
using CommonTestUtilities.Tokens;
using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.PurchaseOrders.Update
{
    public class UpdatePurchaseOrderTest : RDTrackRClassFixture
    {
        private const string METHOD = "purchaseorder";
        private readonly Guid _userIdentifier;
        private long _purchaseOrderId;
        public UpdatePurchaseOrderTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _purchaseOrderId = factory.GetPurchaseOrderId();
        }

        //[Fact]
        //public async Task Success()
        //{
        //    var request = RequestUpdatePurchaseOrderItemsJsonBuilder.Build();
        //    var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        //    var response = await DoPut(method: $"{METHOD}/{_purchaseOrderId}/items", request: request, token: token);

        //    response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        //}
    }
}
