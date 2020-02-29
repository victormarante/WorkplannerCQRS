using System;
using System.Threading.Tasks;
using Shouldly;
using WorkplannerCQRS.API.Domain.Worker.Commands;
using WorkplannerCQRS.API.Domain.Worker.Models;
using WorkplannerCQRS.API.Domain.Worker.Queries;
using WorkplannerCQRS.IntegrationTests.Setup;
using Xunit;

namespace WorkplannerCQRS.IntegrationTests.WorkerTests
{
    using static TestFixture;
    
    public class DeleteWorkerTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldDeleteWorkerTest()
        {
            // Arrange
            var worker = new Worker
            {
                Id = Guid.NewGuid(),
                Email = "Email@email.com",
                Name = "Name Nameson",
                PhoneNumber = "0700123123",
            };

            await InsertAsync<Worker>(worker);
            var query = new GetWorkerByIdQuery(worker.WorkerId);
            var command = new DeleteWorkerCommand(worker.WorkerId);

            // Act
            var getBeforeDeletion = await SendRequestAsync(query); 
            var deleteCommandResult = await SendRequestAsync(command);
            var getQueryResult = await SendRequestAsync(query);
            
            // Assert
            
            // Asserting before delete command
            getBeforeDeletion.Success.ShouldBeTrue();
            deleteCommandResult.Data.ShouldNotBeNull();
            deleteCommandResult.Message.ShouldBeNullOrEmpty();
            
            // Asserting delete command
            deleteCommandResult.Success.ShouldBeTrue();
            deleteCommandResult.Data.ShouldNotBeNull();
            deleteCommandResult.Message.ShouldBeNullOrEmpty();
            
            // Asserting query command
            getQueryResult.Data.ShouldBeNull();
            getQueryResult.Message.ShouldNotBeNullOrEmpty();
            getQueryResult.Success.ShouldBeFalse();
        }
    }
}