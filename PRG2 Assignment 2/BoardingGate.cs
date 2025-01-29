using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_2
{
    using System;

    namespace PRG2_Assignment_2
    {
        public class BoardingGate
        {
            private string GateName { get; set; }
            private bool SupportsCFFT { get; set; }
            private bool SupportsDDJB { get; set; }
            private bool SupportsLWTT { get; set; }
            private Flight? Flight { get; set; }

            public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT, Flight? flight = null)
            {
                GateName = gateName;
                SupportsCFFT = supportsCFFT;
                SupportsDDJB = supportsDDJB;
                SupportsLWTT = supportsLWTT;
                Flight = flight;
            }
            public double CalculateFees() => Flight?.CalculateFees() ?? 0.0;
            public string GetGateName() => GateName;
            public bool GetSupportsCFFT() => SupportsCFFT;
            public bool GetSupportsDDJB() => SupportsDDJB;
            public bool GetSupportsLWTT() => SupportsLWTT;
            public Flight? GetAssignedFlight() => Flight;
            public override string ToString()
            {
                return $"Gate Name: {GateName}, Supports CFFT: {SupportsCFFT}, Supports DDJB: {SupportsDDJB}, Supports LWTT: {SupportsLWTT}, Assigned Flight: {Flight?.GetFlightNumber() ?? "None"}";
            }
        }
    }

}
