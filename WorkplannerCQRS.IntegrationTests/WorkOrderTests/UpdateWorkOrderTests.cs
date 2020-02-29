using System;
using System.Threading.Tasks;
using Shouldly;
using WorkplannerCQRS.API.Domain.WorkOrder.Commands;
using WorkplannerCQRS.API.Domain.WorkOrder.Enums;
using WorkplannerCQRS.API.Domain.WorkOrder.Models;
using WorkplannerCQRS.IntegrationTests.Setup;
using Xunit;

namespace WorkplannerCQRS.IntegrationTests.WorkOrderTests
{
    using static TestFixture;
    
    public class UpdateWorkOrderTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldUpdateWorkOrderTest()
        {
            // Arrange
            var createWorkOrderCommand = new CreateWorkOrderCommand
            {
                Address = "Address 123",
                Description = "Description 123",
                ObjectNumber = $"B{NextObjectNumber()}",
                StartDate = DateTimeOffset.Now.AddDays(1),
                EndDate = DateTimeOffset.Now.AddDays(5),
                Status = WorkOrderStatus.NotStarted,
            };

            var createWorkOrderCommandResult = await SendRequestAsync(createWorkOrderCommand);

            // Act
            var updateWorkOrderCommand = new UpdateWorkOrderCommand(
                createWorkOrderCommandResult.Data.ObjectNumber,
                "Address 123 UPDATED",
                "Description 123 UPDATED",
                DateTimeOffset.Now.AddDays(1),
                DateTimeOffset.Now.AddDays(5),
                WorkOrderStatus.Started);

            var result = await SendRequestAsync(updateWorkOrderCommand);
            var workOrder = await FindAsync<WorkOrder>(result.Data.ObjectNumber);

            // Assert
            result.Success.ShouldBeTrue();
            result.Message.ShouldBeEmpty();
            
            workOrder.ObjectNumber.ShouldBe(updateWorkOrderCommand.ObjectNumber);
            workOrder.Address.ShouldBe(updateWorkOrderCommand.Address);
            workOrder.Description.ShouldBe(updateWorkOrderCommand.Description);
            workOrder.StartDate.ShouldBe(updateWorkOrderCommand.StartDate);
            workOrder.EndDate.ShouldBe(updateWorkOrderCommand.EndDate);
            workOrder.Status.ShouldBe(updateWorkOrderCommand.Status);
        }
    }
}