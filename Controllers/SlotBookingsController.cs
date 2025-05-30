using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TailTeamAPI.Data;
using TailTeamAPI.Models;
using TailTeamAPI.Services;

namespace TailTeamAPI.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class SlotBookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SlotService _slotService;

        public SlotBookingsController(ApplicationDbContext context, SlotService slotService)
        {
            _context = context;
            _slotService = slotService;
        }




        // GET: api/SlotBookings
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SlotBooking>>> GetSlotBookings()
        {
            return await _context.SlotBookings.ToListAsync();
        }

        // GET: api/SlotBookings/
        [HttpGet("GetBookedSlot/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SlotBooking>> GetSlotBooking(int id)
        {
            var slotBooking = await _context.SlotBookings.FindAsync(id);

            if (slotBooking == null)
            {
                return NotFound();
            }

            return slotBooking;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<ActionResult<List<SlotBooking>>> GetUserBookedSlots(string userId)
        {
            var bookedSlots = await _context.SlotBookings.Where( x => x.CustomerId == userId && x.Status != "cancelled").ToListAsync();

            if (bookedSlots == null)
            {
                return NotFound();
            }

            return Ok(bookedSlots);
        }

        [HttpGet("available-slots/{consultantId}")]
        public IActionResult GetAvailableSlots(int consultantId)
        {
            var slots = _slotService.GenerateAvailableSlots(consultantId);
            return Ok(slots);
        }


        // cancel booking

        // PUT: api/SlotBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("cancel/{id}")]
        [Authorize]
        public async Task<IActionResult> cancelSlotBooking(int id)
        {

            var booking = await _context.SlotBookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = "cancelled";

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotBookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SlotBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SlotBooking>> PostSlotBooking(SlotBooking slotBooking)
        {

            if (slotBooking == null)
                return BadRequest("Invalid data");
            try
            {
                _context.SlotBookings.Add(slotBooking);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Booking failed: " + ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return CreatedAtAction("GetSlotBooking", new { id = slotBooking.Id }, slotBooking);
        }

        // DELETE: api/SlotBookings/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSlotBooking(int id)
        {
            var slotBooking = await _context.SlotBookings.FindAsync(id);
            if (slotBooking == null)
            {
                return NotFound();
            }

            _context.SlotBookings.Remove(slotBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlotBookingExists(int id)
        {
            return _context.SlotBookings.Any(e => e.Id == id);
        }
    }
}
