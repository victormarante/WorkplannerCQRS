using System;
using System.Threading.Tasks;
using Shouldly;
using WorkplannerCQRS.API.Domain.Worker.Models;
using WorkplannerCQRS.API.Domain.WorkOrder.Commands;
using WorkplannerCQRS.API.Domain.WorkOrder.Enums;
using WorkplannerCQRS.API.Domain.WorkOrder.Models;
using WorkplannerCQRS.API.Domain.WorkOrderWorker.Models;
using WorkplannerCQRS.IntegrationTests.Setup;
using Xunit;

namespace WorkplannerCQRS.IntegrationTests
{
    using static TestFixture;
    
    public class WorkOrderWorkerAddHoursCommandTests : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldAddWorkOrderWorker_Should_Add_Successfully()
        {
            // arrange
            var worker = new Worker
            {
                Id = Guid.NewGuid(),
                Email = "Email@email.com",
                Name = "Name Nameson",
                PhoneNumber = "0700123123",
            };
            
            var workOrder = new WorkOrder
            {
                Address = "Address 123",
                Description = "Description 123",
                ObjectNumber = $"B{NextObjectNumber()}",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(5),
                Status = WorkOrderStatus.NotStarted,
            };

            await InsertAsync(worker);
            await InsertAsync(workOrder);
            
            var addWorkHoursCommand = new AddWorkHoursCommand()
            {
                WorkerId = worker.WorkerId,
                ObjectNumber = workOrder.ObjectNumber,
                HoursWorked = 10,
                DateTime = DateTime.UtcNow,
            };

            // act
            await SendRequestAsync(addWorkHoursCommand);
            var result = await FindAsync<WorkOrderWorker>(addWorkHoursCommand.ObjectNumber, addWorkHoursCommand.WorkerId);
            
            //assert
            result.ShouldNotBeNull();
            result.WorkerId = addWorkHoursCommand.WorkerId;
            result.DateTime = addWorkHoursCommand.DateTime;
            result.ObjectNumber = addWorkHoursCommand.ObjectNumber;
            result.HoursWorked = addWorkHoursCommand.HoursWorked;
        }
    }
}