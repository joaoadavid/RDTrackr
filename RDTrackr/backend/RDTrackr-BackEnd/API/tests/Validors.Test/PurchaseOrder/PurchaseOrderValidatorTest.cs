using CommonTestUtilities.Requests.PurchaseOrder;
using RDTrackR.Application.UseCases.PurchaseOrders;
using RDTrackR.Exceptions;
using Shouldly;

namespace Validors.Test.PurchaseOrder
{
    public class PurchaseOrderValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new PurchaseOrderValidator();
            var request = RequestPurchaseOrderJsonBuilder.Build();
            var result = validator.Validate(request);
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Error_Supplier_Empty()
        {
            var validator = new PurchaseOrderValidator();
            var request = RequestPurchaseOrderJsonBuilder.Build();
            request.SupplierId = 0;

            var result = validator.Validate(request);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldHaveSingleItem();
            result.Errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.SUPPLIER_REQUIRED));
        }

        [Fact]
        public void Error_Items_Empty()
        {
            var validator = new PurchaseOrderValidator();
            var request = RequestPurchaseOrderJsonBuilder.Build();
            request.Items = [];

            var result = validator.Validate(request);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldHaveSingleItem();
            result.Errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.PO_ITEMS_REQUIRED));
        }
    }
}
