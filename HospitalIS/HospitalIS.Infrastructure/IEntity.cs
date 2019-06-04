using System;

namespace HospitalIS.Infrastructure
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string TableName { get; }
    }
}
