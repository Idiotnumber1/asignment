using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_2
{
    public abstract class Flight
    {
        private string FlightNumber { get; set; }
        private string Origin { get; set; }
        private string Destination { get; set; }
        private string Status { get; set; }
        private DateTime ExpectedTime { get; set; }

        public Flight(string flightNumber, string origin, string destination, string status, DateTime expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            Status = status;
            ExpectedTime = expectedTime;
        }

        public string GetFlightNumber() => FlightNumber;
        public string GetOrigin() => Origin;
        public string GetDestination() => Destination;
        public string GetStatus() => Status;
        public DateTime GetExpectedTime() => ExpectedTime;

        public abstract double CalculateFees();

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Status: {Status}, Expected Time: {ExpectedTime:dd/MM/yyyy hh:mm tt}";
        }
    }

}
