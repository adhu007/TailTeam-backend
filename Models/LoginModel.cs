using System.ComponentModel.DataAnnotations.Schema;

namespace TailTeamAPI.Models
{
    public class LoginModel
    {

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
    }
}
