using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Entity.Dto
{
    public record EmailConfirmationDto
    {
        public User? User { get; init; }
        public string? Email { get; init; }
        public string? ConfirmationLink { get; init; }
    }
}
