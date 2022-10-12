using Microsoft.AspNetCore.Identity;

namespace Chat.DataAccess.Entities
{
    public class UserEntity: IdentityUser<long>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
