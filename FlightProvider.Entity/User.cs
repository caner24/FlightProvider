using FlightProvider.Domain.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Entity
{
    public class User : IdentityUser, IEntity
    {
        public User()
        {
            Flight = new List<Flight>();
        }
        public List<Flight> Flight { get; set; }
    }
}
