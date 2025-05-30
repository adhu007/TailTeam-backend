using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TailTeamAPI.Models;

namespace TailTeamAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<SlotBooking> SlotBookings { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SlotBooking>()
                .HasOne(sb => sb.Customer)
                .WithMany()
                .HasForeignKey(sb => sb.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SlotBooking>()
                .HasOne(sb => sb.Consultant)
                .WithMany()
                .HasForeignKey(sb => sb.ConsultantId);

            modelBuilder.Entity<PaymentDetails>()
                .HasOne<SlotBooking>()
                .WithOne(sb => sb.Payment)
                .HasForeignKey<PaymentDetails>(p => p.SlotBookingId);

            modelBuilder.Entity<SlotBooking>()
                .HasIndex(sb => new { sb.ConsultantId, sb.SlotTime })
                .IsUnique(); //  Prevents double booking
        }
    }
}
