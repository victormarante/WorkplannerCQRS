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
    
    public class CreateWorkOrderTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldCreateWorkOrder()
        {
            // Arrange
            var command = new CreateWorkOrderCommand(
                $"B{NextObjectNumber()}",
                "Address 123",
                "Description 123",
                DateTimeOffset.Now,
                DateTimeOffset.Now.AddDays(5),
                WorkOrderStatus.NotStarted);

            // Act
            var commandResult = await SendRequestAsync(command);
            var workOrder = await FindAsync<WorkOrder>(commandResult.Data.ObjectNumber);

            // Assert
            workOrder.ShouldNotBeNull();
            workOrder.ObjectNumber.ShouldBe(command.ObjectNumber);
            workOrder.Address.ShouldBe(command.Address);
            workOrder.Description.ShouldBe(command.Description);
        }
    }
}