using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Role
    {
        [Key]
        [MaxLength(450)]
        public string Name { get; set; }
        public ICollection<Right> Rights { get; set; }
        public ICollection<User> Users { get; set; }
    }
}