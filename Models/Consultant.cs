using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TailTeamAPI.Models
{
    public class Consultant
    {

        [Key]
        public int Id { get; set; }


        [Required]

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ImageName { get; set; }

        [Required]

        [Column(TypeName = "nvarchar(50)")]
        public string Speciality { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public string? Degree { get; set; }


        [Column(TypeName = "nvarchar(250)")]
        public string? About { get; set; }

        [Required]
        public Decimal AppointmentFee { get; set; }



    }
}
