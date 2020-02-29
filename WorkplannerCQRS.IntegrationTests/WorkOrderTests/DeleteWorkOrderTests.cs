using System;
using System.Threading.Tasks;
using Shouldly;
using WorkplannerCQRS.API.Domain.WorkOrder.Commands;
using WorkplannerCQRS.API.Domain.WorkOrder.Enums;
using WorkplannerCQRS.API.Domain.WorkOrder.Models;
using WorkplannerCQRS.API.Domain.WorkOrder.Queries;
using WorkplannerCQRS.IntegrationTests.Setup;
using Xunit;

namespace WorkplannerCQRS.IntegrationTests.WorkOrderTests
{
    using static TestFixture;
    
    public class DeleteWorkOrderTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldDeleteWorkOrder()
        {
            // Arrange
            var workOrder = new WorkOrder
            {
                Address = "Address 123",
                Description = "Description 123",
                ObjectNumber = $"B{NextObjectNumber()}",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(5),
                Status = WorkOrderStatus.NotStarted,
            };
            
            await InsertAsync(workOrder);
            var deleteWorkOrderCommand = new DeleteWorkOrderCommand(workOrder.ObjectNumber);
            var query = new GetWorkOrderByIdQuery(workOrder.ObjectNumber);
            
            // Act
            var deleteCommandResult = await SendRequestAsync(deleteWorkOrderCommand);
            var getQueryResult = await SendRequestAsync(query);
            
            // Assert
            
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