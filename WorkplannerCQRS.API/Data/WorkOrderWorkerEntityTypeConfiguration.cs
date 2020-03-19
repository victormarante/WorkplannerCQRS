using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkplannerCQRS.API.Domain.WorkOrderWorker.Models;

namespace WorkplannerCQRS.API.Data
{
    public class WorkOrderWorkerEntityTypeConfiguration : IEntityTypeConfiguration<WorkOrderWorker>
    {
        public void Configure(EntityTypeBuilder<WorkOrderWorker> builder)
        {
            builder.HasKey(x => new {x.ObjectNumber, x.WorkerId});

            builder.HasOne(x => x.Worker)
                .WithMany(x => x.WorkOrderWorkers)
                .HasForeignKey(x => x.WorkerId);
            
            builder.HasOne(x => x.WorkOrder)
                .WithMany(x => x.WorkOrderWorkers)
                .HasForeignKey(x => x.ObjectNumber);
        }
    }
}