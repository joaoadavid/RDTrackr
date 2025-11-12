using CommonTestUtilities.Requests;
using FluentAssertions;
using RDTrackR.Application.UseCases.Products;
using RDTrackR.Exceptions;
using Shouldly;

namespace Validors.Test.Product
{
    public class ProductValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ProductValidator();
            var request = RequestProductJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
            result.Errors.ShouldBeEmpty();
        }

        [Fact]
        public void Error_SKU_Empty()
        {
            var validator = new ProductValidator();
            var request = RequestProductJsonBuilder.Build();
            request.Sku = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.PRODUCT_SKU_REQUIRED);
        }

        [Fact]
        public void Error_Product_Name_Empty()
        {
            var validator = new ProductValidator();
            var request = RequestProductJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.PRODUCT_NAME_REQUIRED);
        }

        [Fact]
        public void Error_Category_Empty()
        {
            var validator = new ProductValidator();
            var request = RequestProductJsonBuilder.Build();
            request.Category = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.PRODUCT_CATEGORY_REQUIRED);
        }

        [Fact]
        public void Error_Product_Empty()
        {
            var validator = new ProductValidator();
            var request = RequestProductJsonBuilder.Build();
            request.UoM = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.PRODUCT_UNIT_REQUIRED);
        }

        [Fact]
        public void Error_Price_Empty()
        {
            var validator = new ProductValidator();
            var request = RequestProductJsonBuilder.Build();
            request.Price = -1;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.PRODUCT_PRICE_INVALID);
        }

        [Fact]
        public void Error_Stock_Empty()
        {
            var validator = new ProductValidator();
            var request = RequestProductJsonBuilder.Build();
            request.Stock = -1;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.PRODUCT_STOCK_INVALID);
        }

        [Fact]
        public void Error_Reorder_Empty()
        {
            var validator = new ProductValidator();
            var request = RequestProductJsonBuilder.Build();
            request.ReorderPoint = -1;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.PRODUCT_REORDER_INVALID);
        }
    }
}
