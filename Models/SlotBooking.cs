using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TailTeamAPI.Models
{
    public class SlotBooking
    {

        [Key]
        public int Id { get; set; }


       
        public string CustomerId { get; set; }
        public int ConsultantId { get; set; }

        [Required]
        public DateTime SlotDate { get; set; } // Exact date + time of slot
        public TimeSpan SlotTime { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; } // Booked, Cancelled

        [JsonIgnore]
        public ApplicationUser? Customer { get; set; }

        [JsonIgnore]
        public Consultant? Consultant { get; set; }

        public PaymentDetails? Payment { get; set; }
    }
}
