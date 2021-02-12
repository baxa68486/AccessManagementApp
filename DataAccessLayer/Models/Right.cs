using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Right
    {
        [Key]
        [MaxLength(450)]
        public string Name { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}