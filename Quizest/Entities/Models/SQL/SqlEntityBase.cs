using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models.SQL
{
    public abstract class SqlEntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
