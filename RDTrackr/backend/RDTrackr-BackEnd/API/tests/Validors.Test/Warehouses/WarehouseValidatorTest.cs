using CommonTestUtilities.Requests.Warehouse;
using FluentAssertions;
using RDTrackR.Application.UseCases.Warehouses;
using RDTrackR.Exceptions;
using Shouldly;

namespace Validors.Test.Warehouses
{
    public class WarehouseValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new WarehouseValidator();
            var request = RequestRegisterWarehouseJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
            result.Errors.ShouldBeEmpty();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new WarehouseValidator();
            var request = RequestRegisterWarehouseJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.WAREHOUSE_NAME_REQUIRED);
        }

        [Fact]
        public void Error_Location_Empty()
        {
            var validator = new WarehouseValidator();
            var request = RequestRegisterWarehouseJsonBuilder.Build();
            request.Location = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.WAREHOUSE_LOCATION_REQUIRED);
        }

        [Fact]
        public void Error_Capacity_Invalid()
        {
            var validator = new WarehouseValidator();
            var request = RequestRegisterWarehouseJsonBuilder.Build();
            request.Capacity = 0;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.WAREHOUSE_CAPACITY_INVALID);
        }

        [Fact]
        public void Error_Items_Invalid_Negative()
        {
            var validator = new WarehouseValidator();
            var request = RequestRegisterWarehouseJsonBuilder.Build();
            request.Items = -1;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.ErrorMessage == ResourceMessagesException.WAREHOUSE_ITEMS_INVALID);
        }
    }
}
