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
    
    public class GetWorkOrderTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldGetWorkOrderByObjectNumber()
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

            var query = new GetWorkOrderByIdQuery(workOrder.ObjectNumber);

            // Act
            var createWorkOrderCommandResponse = await SendRequestAsync(query);
            var actualWorkOrder = await FindAsync<WorkOrder>(workOrder.ObjectNumber);
            
            // Assert
            createWorkOrderCommandResponse.ShouldNotBeNull();
            actualWorkOrder.ShouldNotBeNull();
            actualWorkOrder.Id.ShouldBe(workOrder.Id);
            actualWorkOrder.Address.ShouldBe(workOrder.Address);
            actualWorkOrder.Description.ShouldBe(workOrder.Description);
            actualWorkOrder.ObjectNumber.ShouldBe(workOrder.ObjectNumber);
        }

        [Fact]
        public async Task ShouldGetAllWorkOrders()
        {
            // Arrange
            var query = new GetAllWorkOrdersQuery();
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
            
            // Act
            var result = await SendRequestAsync(query);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.ShouldNotBeEmpty();
            result.Message.ShouldBeNullOrEmpty();
            result.Data.ShouldContain(x => x.ObjectNumber == workOrder.ObjectNumber);
            result.Data.ShouldContain(x => x.Address == workOrder.Address);
            result.Data.ShouldContain(x => x.Description == workOrder.Description);
            result.Data.ShouldContain(x => x.Status == workOrder.Status);
            result.Data.ShouldContain(x => x.StartDate == workOrder.StartDate);
            result.Data.ShouldContain(x => x.EndDate == workOrder.EndDate);
        }
    }
}