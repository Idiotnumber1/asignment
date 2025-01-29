using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRG2_Assignment_2.PRG2_Assignment_2;

namespace PRG2_Assignment_2
{

    public class Terminal
    {
        private string TerminalName { get; set; }
        private Dictionary<string, Airline> Airlines { get; set; } = new();
        private Dictionary<string, BoardingGate> BoardingGates { get; set; } = new();
        private Dictionary<string, double> GateFees { get; set; } = new();

        public Terminal(string terminalName, Dictionary<string, Airline> airlines, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, double> gateFees)
        {
            TerminalName = terminalName;
            Airlines = airlines;
            BoardingGates = boardingGates;
            GateFees = gateFees;
        }

        public string GetTerminalName() => TerminalName;

        public Dictionary<string, Airline> GetAirlines() => Airlines;

        public Dictionary<string, BoardingGate> GetBoardingGates() => BoardingGates;

        public bool AddAirline(Airline airline)
        {
            if (!Airlines.ContainsKey(airline.GetCode()))
            {
                Airlines.Add(airline.GetCode(), airline);
                return true;
            }
            return false;
        }

        public bool AddBoardingGate(BoardingGate gate)
        {
            if (!BoardingGates.ContainsKey(gate.GetGateName()))
            {
                BoardingGates.Add(gate.GetGateName(), gate);
                return true;
            }
            return false;
        }

        

        public Airline? GetAirlineFromFlight(Flight flight)
        {
            foreach (var airline in Airlines.Values)
            {
                if (airline.GetFlights().ContainsKey(flight.GetFlightNumber()))
                    return airline;
            }
            return null;
        }

        public void PrintAirlineFees()
        {
            foreach (var airline in Airlines.Values)
            {
                Console.WriteLine($"Airline: {airline.GetName()}, Fees: {airline.CalculateFees()}");
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Terminal Name: {TerminalName}");
            sb.AppendLine("Airlines:");
            foreach (var airline in Airlines.Values)
            {
                sb.AppendLine($" - {airline.ToString()}");
            }
            sb.AppendLine("Boarding Gates:");
            foreach (var gate in BoardingGates.Values)
            {
                sb.AppendLine($" - {gate.ToString()}");
            }
            sb.AppendLine("Gate Fees:");
            foreach (var fee in GateFees)
            {
                sb.AppendLine($" - {fee.Key}: {fee.Value:C}");
            }
            return sb.ToString();
        }
    }
}
