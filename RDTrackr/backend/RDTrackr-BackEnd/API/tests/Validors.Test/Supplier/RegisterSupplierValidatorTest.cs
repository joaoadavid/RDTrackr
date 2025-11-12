using CommonTestUtilities.Requests.Supplier;
using FluentAssertions;
using RDTrackR.Application.UseCases.Suppliers.Register;
using RDTrackR.Exceptions;
using Shouldly;

namespace Validors.Test.Supplier
{
    public class RegisterSupplierValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterSupplierValidator();
            var request = RequestRegisterSupplierJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new RegisterSupplierValidator();
            var request = RequestRegisterSupplierJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.SUPPLIER_NAME_REQUIRED);
        }
    }
}
