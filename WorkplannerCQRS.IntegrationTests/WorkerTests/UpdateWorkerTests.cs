using System.Threading.Tasks;
using Shouldly;
using WorkplannerCQRS.API.Domain.Worker.Commands;
using WorkplannerCQRS.API.Domain.Worker.Models;
using WorkplannerCQRS.API.Domain.WorkOrder.Models;
using WorkplannerCQRS.IntegrationTests.Setup;
using Xunit;

namespace WorkplannerCQRS.IntegrationTests.WorkerTests
{
    using static TestFixture;
    
    public class UpdateWorkerTests
    {
        [Fact]
        public async Task ShouldUpdateWorkerTest()
        {
            // Arrange
            var createWorkerCommand = new CreateWorkerCommand
            {
                Name = "Test Testson",
                Email = "test@test.com",
                PhoneNumber = "0700-123123",
            };

            var createWorkerCommandResult = await SendRequestAsync(createWorkerCommand);

            // Act
            var updateWorkerCommand = new UpdateWorkerCommand(
                createWorkerCommandResult.Data.WorkerId,
                "Updted name",
                "Updated Email",
                "Updated PhoneNumber");

            var result = await SendRequestAsync(updateWorkerCommand);
            var worker = await FindAsync<Worker>(result.Data.WorkerId);

            // Assert
            result.Success.ShouldBeTrue();
            result.Message.ShouldBeEmpty();
            worker.WorkerId.ShouldBe(updateWorkerCommand.WorkerId);
            worker.Email.ShouldBe(updateWorkerCommand.Email);
            worker.PhoneNumber.ShouldBe(updateWorkerCommand.PhoneNumber);
        }
    }
}