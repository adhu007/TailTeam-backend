using System.ComponentModel.DataAnnotations.Schema;

namespace TailTeamAPI.Models
{
    public class RegisterModel
    {


        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }  // "User" or "Admin"

    }
}
