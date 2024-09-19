using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Entity.Dto
{
  public  record FlightTicketDto
    {
        public string Email { get; init; }
        public string FlightNumber { get; init; }

    }
}
