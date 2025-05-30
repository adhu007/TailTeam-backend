using TailTeamAPI.Data;

namespace TailTeamAPI.Services
{
    public class SlotService
    {
        private readonly ApplicationDbContext _context;

        public SlotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IEnumerable<object>> GenerateAvailableSlots(int consultantId)
        {
            var istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istTimeZone);
            var result = new List<List<object>>();

            for (int day = 0; day < 7; day++)
            {
                var currentDaySlots = new List<object>();
                var currentDate = now.Date.AddDays(day);

                for (int hour = 10; hour < 19; hour++) // From 10:00 AM to 6:00 PM
                {
                    var slot = currentDate.AddHours(hour);

                    if (day == 0 && slot <= now)
                    {
                        continue;
                    }
                    // Convert slot to UTC for comparison with database
                    var slotUtc = TimeZoneInfo.ConvertTimeToUtc(slot, istTimeZone);

                    bool isBooked = _context.SlotBookings.Any(sb =>
                            sb.ConsultantId == consultantId &&
                            sb.SlotDate.Date == slot.Date &&
                            sb.SlotTime == slot.TimeOfDay &&
                            sb.Status != "cancelled"
                        );


                    if (!isBooked)
                    {
                        currentDaySlots.Add(new
                        {
                            dateTime = slotUtc.ToString("o"),
                            time = slot.ToString("hh:mm tt")
                        });
                    }
                }

                result.Add(currentDaySlots);
            }

            return result;
        }
    }
}
