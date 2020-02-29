using System;

namespace WorkplannerCQRS.API.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        
        public DateTimeOffset ModifiedAt { get; set; }
    }
}