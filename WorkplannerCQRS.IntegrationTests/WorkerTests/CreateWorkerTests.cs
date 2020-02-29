using System.Threading.Tasks;
using Shouldly;
using WorkplannerCQRS.API.Domain.Worker;
using WorkplannerCQRS.API.Domain.Worker.Commands;
using WorkplannerCQRS.IntegrationTests.Setup;
using Xunit;

namespace WorkplannerCQRS.IntegrationTests.WorkerTests
{
    using static TestFixture;
    
    public class CreateWorkerTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldCreateWorkerTest()
        {
            var command = new CreateWorkerCommand("Name", "Email", "phoneNumber");
            var commandResult = await SendRequestAsync(command);
            
            commandResult.ShouldNotBeNull();
            commandResult.Data.ShouldNotBeNull();
            commandResult.Message.ShouldBeNullOrEmpty();
            commandResult.Success.ShouldBeTrue();

            var worker = await FindAsync<Worker>(commandResult.Data.WorkerId);
            
            worker.Name.ShouldBe(command.Name);
            worker.Email.ShouldBe(command.Email);
            worker.PhoneNumber.ShouldBe(command.PhoneNumber);
        }
    }
}