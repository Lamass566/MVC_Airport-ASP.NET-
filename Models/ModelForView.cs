using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class ModelForView
    {
        public IEnumerable<AirportsArrival> AirportsArrival { get; set; }
        public IEnumerable<AirportsDeparture> AirportsDeparture { get; set; }
        public IEnumerable<Passengers> Passengers { get; set; }
    }
}