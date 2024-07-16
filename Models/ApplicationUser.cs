using Microsoft.AspNetCore.Identity;

namespace IsTakipSistemi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public string? ProfilePictureUrl { get; set; }   //kullanıcı profil resmi koymak isterse linki
        public ICollection<UserRole>? UserRoles { get; set; } // kullanıcının rolü 
        public ICollection<Project>? Projects { get; set; } // Kullanıcı tarafından oluşturulan projeler
        public ICollection<Models.Task>? AssignedTasks { get; set; } // kullanıcıya atanan görevler
        public ICollection<CalendarEvent>? CalendarEvents { get; set; } // kullanıcının görev ve projelerinin takvimi
        public ApplicationUser()
        {
                
        }
    }

}
