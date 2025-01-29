using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_2
{
    public class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        public CFFTFlight(string flightNumber, string origin, string destination, string status, DateTime expectedTime, double requestFee)
            : base(flightNumber, origin, destination, status, expectedTime)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees() => 1000.0 + RequestFee;

        public override string ToString()
        {
            return $"{base.ToString()}, Request Fee: {RequestFee:C}, Flight Type: CFFT Flight";
        }
    }


}
