using System.Collections.Generic;

namespace DataAccessLayer.DTOs
{
    public class UserDTO
    {
        public string LoginName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public List<RoleDTO> Roles { get; set; }
    }
}
