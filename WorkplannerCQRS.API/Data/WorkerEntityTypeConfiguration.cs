using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkplannerCQRS.API.Domain.Worker;

namespace WorkplannerCQRS.API.Data
{
    public class WorkerEntityTypeConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasKey(x => x.WorkerId);
            builder.Property(x => x.WorkerId).ValueGeneratedOnAdd();
            
            builder.ToTable(nameof(Worker));
        }
    }
}