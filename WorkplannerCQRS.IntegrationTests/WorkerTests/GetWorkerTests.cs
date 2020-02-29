using System;
using System.Threading.Tasks;
using Shouldly;
using WorkplannerCQRS.API.Domain.Worker;
using WorkplannerCQRS.API.Domain.Worker.Queries;
using WorkplannerCQRS.IntegrationTests.Setup;
using Xunit;

namespace WorkplannerCQRS.IntegrationTests.WorkerTests
{
    using static TestFixture;
    public class GetWorkerTests
    {
        [Fact]
        public async Task ShouldGetWorkerById()
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
            
            // Act
            var result = await SendRequestAsync(query);

            // Assert
            result.ShouldNotBeNull();
            result.Data.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.Message.ShouldBeNullOrEmpty();
            
            result.Data.WorkerId.ShouldBe(worker.WorkerId);
            result.Data.Name.ShouldBe(worker.Name);
            result.Data.Email.ShouldBe(worker.Email);
            result.Data.PhoneNumber.ShouldBe(worker.PhoneNumber);
        }
    }
}