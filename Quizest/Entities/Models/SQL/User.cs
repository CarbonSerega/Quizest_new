using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Converters;

namespace Entities.Models.SQL
{
    [Table(nameof(User))]
    public class User : SqlEntityBase
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string AvatarPath { get; set; }

        [Required]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public Role Role { get; set; }
    }
}
