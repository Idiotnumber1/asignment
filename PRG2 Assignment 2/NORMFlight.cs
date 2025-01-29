using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_2
{
    public class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, string status, DateTime expectedTime)
            : base(flightNumber, origin, destination, status, expectedTime)
        {
        }
        public override double CalculateFees() => 500.0;
        public override string ToString()
        {
            return $"{base.ToString()}, Flight Type: Normal Flight";
        }
    }


}
