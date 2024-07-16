namespace IsTakipSistemi.Models
{
    public class UserRole
    {
         // kullanıcıların rollerini atamak için kullanılan class
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string? RoleId { get; set; }
        public ApplicationRole? Role { get; set; }
        public UserRole()
        {
            
        }
    }

}
