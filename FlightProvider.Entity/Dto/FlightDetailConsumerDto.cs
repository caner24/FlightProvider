using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Entity.Dto
{
    public record FlightDetailConsumerDto
    {
        public string? Email { get; init; }
        public DateOnly? DepartureDate { get; init; }
        public TimeSpan? DepartureTime { get; init; }
        public DateOnly ArrivalDate { get; init; }
        public TimeSpan ArrivalTime { get; init; }
        public string? DepartureCity { get; init; }
        public string? ArrivalCity { get; init; }
        public string? FlightNo { get; init; }

        public int Amount { get; init; }

        public double TotalPrice { get; set; }
    }
}
