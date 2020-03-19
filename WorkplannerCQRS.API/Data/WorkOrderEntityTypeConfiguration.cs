using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkplannerCQRS.API.Domain.WorkOrder.Models;

namespace WorkplannerCQRS.API.Data
{
    public class WorkOrderEntityTypeConfiguration : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(EntityTypeBuilder<WorkOrder> builder)
        {
            builder.HasKey(x => x.ObjectNumber);
            builder.Property(x => x.Id);

            builder.ToTable(nameof(WorkOrder));
        }
    }
}