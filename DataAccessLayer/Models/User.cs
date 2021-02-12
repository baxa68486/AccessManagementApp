using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class User
    {
        [Key]
        public string LoginName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}