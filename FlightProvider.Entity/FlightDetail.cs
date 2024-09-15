using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Entity
{
    public class FlightDetail
    {
        public FlightDetail()
        {
            Users = new List<User>();
        }
        public int FlightId { get; set; }
        public DateOnly DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string? DepartureCity { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public string? ArrivalCity { get; set; }
        public List<User> Users { get; set; }
        public Flight? Flight { get; set; }
    }
}
