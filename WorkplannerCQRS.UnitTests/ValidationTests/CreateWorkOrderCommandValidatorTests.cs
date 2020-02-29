using System;
using System.Drawing;
using System.Linq;
using FluentValidation.TestHelper;
using WorkplannerCQRS.API.Domain.WorkOrder.Validators;
using Xunit;

namespace WorkplannerCQRS.UnitTests.ValidationTests
{
    // TODO: Move each set of tests (i.e. name valid values tests) to own class
    // Motive: Not have both error and non-error values in same test
    public class CreateWorkOrderCommandValidatorTests
    {
        
        private readonly CreateWorkOrderCommandValidator _validator;
        
        public CreateWorkOrderCommandValidatorTests()
        {
            _validator = new CreateWorkOrderCommandValidator();
        }
        
        [Fact]
        public void CreateWorkOrderValidationTests_ObjectNumber()
        {
            // error values
            _validator.ShouldHaveValidationErrorFor(x => x.ObjectNumber, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.ObjectNumber, string.Empty);
            
            // non-error values
            _validator.ShouldNotHaveValidationErrorFor(x => x.ObjectNumber, "B1");
            _validator.ShouldNotHaveValidationErrorFor(x => x.ObjectNumber, "B12");
            _validator.ShouldNotHaveValidationErrorFor(x => x.ObjectNumber, "B123");
        }
        
        [Fact]
        public void CreateWorkOrderValidationTests_Address()
        {
            // error values
            var tooLongAddress = new string(Enumerable.Range(0, 51).Select(x => (char) (x + 65)).ToArray());
            _validator.ShouldHaveValidationErrorFor(x => x.Address, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.Address, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.Address, tooLongAddress);
            
            // non-error values
            var address = new string(Enumerable.Range(0, 50).Select(x => (char) (x + 65)).ToArray());
            _validator.ShouldNotHaveValidationErrorFor(x => x.Address, address);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Address, "a");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Address, "1");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Address, "/");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Address, "öäå");
        }

        [Fact]
        public void CreateWorkOrderValidationTests_Description()
        {
            var description = new string(Enumerable.Range(0, 90).Select(x => (char) (x + 33)).ToArray());
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, description);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, "1");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, "12");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, "abc");
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, "öäå");
        }
    }
}