using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Entity.Dto
{
    public record FlightDetailConsumerDto
    {
        public string Email { get; init; }
        public string DepartureDate { get; init; }
        public string DepartureTime { get; init; }
        public string ArrivalDate { get; init; }
        public string ArrivalTime { get; init; }
        public string DepartureCity { get; init; }
        public string ArrivalCity { get; init; }
        public string FlightNo { get; init; }
        public int Amount { get; init; }
        public double TotalPrice { get; init; }
    }
}
