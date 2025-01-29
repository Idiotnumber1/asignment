using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_2
    {
        public class Airline
        {
            private string Name { get; set; }
            private string Code { get; set; }
            private Dictionary<string, Flight> Flights { get; set; } = new();

            public Airline(string name, string code)
            {
                Name = name;
                Code = code;
            }

            public string GetName() => Name;

            public string GetCode() => Code;

            public Dictionary<string, Flight> GetFlights() => Flights;

            public bool AddFlight(Flight flight)
            {
                if (!Flights.ContainsKey(flight.GetFlightNumber()))
                {
                    Flights.Add(flight.GetFlightNumber(), flight);
                    return true;
                }
                return false;
            }

            public bool RemoveFlight(Flight flight)
            {
                return Flights.Remove(flight.GetFlightNumber());
            }

            public double CalculateFees()
            {
                double total = 0;
                foreach (var flight in Flights.Values)
                {
                    total += flight.CalculateFees();
                }
                return total;
            }

            public override string ToString()
            {
                return $"Airline: {Name} ({Code}) | Total Flights: {Flights.Count}";
            }
        }
    }
