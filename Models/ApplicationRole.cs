using Microsoft.AspNetCore.Identity;

namespace IsTakipSistemi.Models
{
    public class ApplicationRole : IdentityRole
    {

         // yeni roller oluşturmak için kullanılacak olan class
        public string? Description { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }   
        
        public ApplicationRole()
        {
                
        }
    }

}
