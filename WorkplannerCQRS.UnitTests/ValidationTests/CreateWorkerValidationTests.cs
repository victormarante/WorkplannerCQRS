using FluentValidation.TestHelper;
using WorkplannerCQRS.API.Domain.Worker.Validators;
using Xunit;

namespace WorkplannerCQRS.UnitTests.ValidationTests
{
    // TODO: Move each set of tests (i.e. name valid values tests) to own class
    // Motive: Not have both error and non-error values in same test
    public class CreateWorkerValidationTests
    {
        private readonly CreateWorkerCommandValidator _validator;

        public CreateWorkerValidationTests()
        {
            _validator = new CreateWorkerCommandValidator();
        }
        
        [Fact]
        public void CreateWorkerCommandValidator_NameTests()
        {
            // error values
            _validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            
            // non-error values
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Hello world");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "123");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Name 123123123123123");
        }

        [Fact]
        public void CreateWorkerCommandValidator_EmailTests()
        {
            // error values
            _validator.ShouldHaveValidationErrorFor(x => x.Email, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.Email, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "asdasd.com");
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "asdsadsa");
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "@asdsadsa.com");
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "asdasdasdsadsa.com");
            
            // non-errors values
            _validator.ShouldNotHaveValidationErrorFor(x => x.Email, "a@d.com");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Email, "real.example.email@bmail.se");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Email, "real.example.email@bmail.org");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Email, "real.example.email@bmail.net");
        }
        
        [Fact]
        public void CreateWorkerCommandValidator_PhoneNumberTests()
        {
            // error values
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, string.Empty);
            
            // non-errors values
            _validator.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, "0700-123123");
        }
    }
}