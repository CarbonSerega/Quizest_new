using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SQL
{
    [Table(nameof(TemporaryLink))]
    public class TemporaryLink : SqlEntityBase
    {
        public string Link { get; set; }
    }
}
