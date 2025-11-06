using CommonTestUtilities.Requests;
using FluentAssertions;
using RDTrackR.Application.UseCases.User.Register;
using RDTrackR.Exceptions;
using Shouldly;

namespace Validors.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]//para a função ser um teste
        public void Success()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            //Assert = verificar se o resultado é o resultado esperado
            //FluentAssertion
            result.IsValid.Should().BeTrue();

            //SHOUDLY
            result.ShouldNotBeNull();
            result.IsValid.ShouldBeTrue();
        }

        [Fact]//para a função ser um teste
        public void Error_Name_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(e=> e.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "email.com";
            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Error_Password_Invalid(int passwordLength)
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();

            // Garante que existe exatamente um erro
            var error = result.Errors.ShouldHaveSingleItem();
            // Verifica se a mensagem do erro é a esperada
            error.ErrorMessage.ShouldBe(ResourceMessagesException.INVALID_PASSWORD);
        }

        [Fact]
        public void Error_Password_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Password = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();

            // Garante que existe exatamente um erro
            var error = result.Errors.ShouldHaveSingleItem();
            // Verifica se a mensagem do erro é a esperada
            error.ErrorMessage.ShouldBe(ResourceMessagesException.PASSWORD_EMPTY);
        }
    }
}

