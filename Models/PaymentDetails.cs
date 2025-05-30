using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TailTeamAPI.Models
{
    public class PaymentDetails
    {

        [Key]
        public int Id { get; set; }

        public int SlotBookingId { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public string? PaymentMode { get; set; }  // Online / Offline

        public Decimal Amount { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; } // Paid, Cancelled, Refunded

        public DateTime? PaymentDate { get; set; }

      
    }
}
