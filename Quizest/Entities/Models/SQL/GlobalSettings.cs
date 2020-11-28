using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models.SQL
{
    [Table(nameof(GlobalSettings))]
    public class GlobalSettings : SqlEntityBase
    {
        public string Locale { get; set; }

        public string Language { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [ForeignKey(nameof(UserId))]
        public Guid? UserId { get; set; }
    }
}
