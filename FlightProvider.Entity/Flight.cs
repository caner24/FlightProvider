using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Entity
{
    public class Flight
    {
        public Flight()
        {
            FlightDetail = new List<FlightDetail>();
        }
        public int Id { get; set; }
        public Guid FlightNo { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<FlightDetail> FlightDetail { get; set; }
    }
}
