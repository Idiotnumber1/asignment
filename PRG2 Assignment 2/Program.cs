//==========================================================
// Student Number : S10267191
// Student Name   : Bertrand Tang
// Partner Name   : Liu Yi Yang
//==========================================================

using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Terminal terminal = new Terminal { TerminalName = "Changi Airport Terminal 5" };
        terminal.LoadFiles("airlines.csv", "boardinggates.csv");

        Console.WriteLine("=============================================");
        Console.WriteLine($"Welcome to {terminal.TerminalName}");
        Console.WriteLine("=============================================");

        while (true)
        {
            Console.WriteLine("1. List All Flights");
            Console.WriteLine("2. List Boarding Gates");
            Console.WriteLine("3. Assign a Boarding Gate to a Flight");
            Console.WriteLine("4. Create Flight");
            Console.WriteLine("5. Display Airline Flights");
            Console.WriteLine("6. Modify Flight Details");
            Console.WriteLine("7. Display Flight Schedule");
            Console.WriteLine("0. Exit");
            Console.Write("\nPlease select your option: ");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ListAllFlights(terminal);
                    break;
                case "2":
                    ListBoardingGates(terminal);
                    break;
                case "3":
                    AssignBoardingGate(terminal);
                    break;
                case "4":
                    CreateFlight(terminal);
                    break;
                case "5":
                    DisplayAirlineFlights(terminal);
                    break;
                case "6":
                    ModifyFlightDetails(terminal);
                    break;
                case "7":
                    DisplayFlightSchedule(terminal);
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void ListAllFlights(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Flights for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");

        foreach (var airline in terminal.Airlines.Values)
        {
            foreach (var flight in airline.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber,-10} {airline.Name,-20} {flight.Origin,-20} {flight.Destination,-20} {flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm tt")}");
            }
        }
    }

    static void ListBoardingGates(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        foreach (var gate in terminal.BoardingGates.Values)
        {
            Console.WriteLine($"{gate.GateName,-10} CFFT: {gate.SupportsCFFT,-5} DDJB: {gate.SupportsDDJB,-5} LWTT: {gate.SupportsLWTT,-5}");
        }
    }

    static void AssignBoardingGate(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Assign a Boarding Gate to a Flight");
        Console.WriteLine("=============================================");
        Console.Write("Enter Flight Number: ");
        string? flightNumber = Console.ReadLine();

        Flight? flight = null;
        Airline? foundAirline = null;
        foreach (var airline in terminal.Airlines.Values)
        {
            if (airline.Flights.TryGetValue(flightNumber!, out flight))
            {
                foundAirline = airline;
                break;
            }
        }

        if (flight == null)
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        Console.Write("Enter Boarding Gate Name: ");
        string? gateName = Console.ReadLine();

        if (terminal.BoardingGates.TryGetValue(gateName!, out var gate))
        {
            if (gate.Flight == null)
            {
                gate.Flight = flight;
                Console.WriteLine($"Flight {flight.FlightNumber} has been assigned to Boarding Gate {gate.GateName}!");
            }
            else
            {
                Console.WriteLine("This gate is already assigned to another flight.");
            }
        }
        else
        {
            Console.WriteLine("Invalid gate name.");
        }
    }

    static void CreateFlight(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Create Flight");
        Console.WriteLine("=============================================");
        Console.Write("Enter Flight Number: ");
        string? flightNumber = Console.ReadLine();

        Console.Write("Enter Origin: ");
        string? origin = Console.ReadLine();

        Console.Write("Enter Destination: ");
        string? destination = Console.ReadLine();

        Console.Write("Enter Expected Departure/Arrival Time (dd/MM/yyyy hh:mm): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime expectedTime);

        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        string? requestCode = Console.ReadLine();

        Flight newFlight;
        switch (requestCode)
        {
            case "CFFT":
                newFlight = new CFFTFlight { FlightNumber = flightNumber!, Origin = origin!, Destination = destination!, ExpectedTime = expectedTime, Status = "Scheduled" };
                break;
            case "DDJB":
                newFlight = new DDJBFlight { FlightNumber = flightNumber!, Origin = origin!, Destination = destination!, ExpectedTime = expectedTime, Status = "Scheduled" };
                break;
            case "LWTT":
                newFlight = new LWTTFlight { FlightNumber = flightNumber!, Origin = origin!, Destination = destination!, ExpectedTime = expectedTime, Status = "Scheduled" };
                break;
            default:
                newFlight = new NORMFlight { FlightNumber = flightNumber!, Origin = origin!, Destination = destination!, ExpectedTime = expectedTime, Status = "Scheduled" };
                break;
        }

        Console.Write("Enter Airline Code: ");
        string? airlineCode = Console.ReadLine();

        if (terminal.Airlines.TryGetValue(airlineCode!, out var airline))
        {
            airline.AddFlight(newFlight);
            Console.WriteLine($"Flight {newFlight.FlightNumber} has been added!");
        }
        else
        {
            Console.WriteLine("Invalid airline code.");
        }
    }

    static void DisplayAirlineFlights(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        foreach (var airline in terminal.Airlines.Values)
        {
            Console.WriteLine($"{airline.Code,-5} {airline.Name}");
        }

        Console.Write("Enter Airline Code: ");
        string? airlineCode = Console.ReadLine();

        if (terminal.Airlines.TryGetValue(airlineCode!, out var selectedAirline))
        {
            foreach (var flight in selectedAirline.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber,-10} {selectedAirline.Name,-20} {flight.Origin,-20} {flight.Destination,-20} {flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm tt")}");
            }
        }
        else
        {
            Console.WriteLine("Invalid airline code.");
        }
    }

    static void ModifyFlightDetails(Terminal terminal)
    {
        // Implementation here
    }

    static void DisplayFlightSchedule(Terminal terminal)
    {
        // Implementation here
    }
}

public class Terminal
{
    public required string TerminalName { get; set; }
    public Dictionary<string, Airline> Airlines { get; set; } = new();
    public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new();
    public Dictionary<string, double> GateFees { get; set; } = new();

    public bool AddAirline(Airline airline)
    {
        if (!Airlines.ContainsKey(airline.Code))
        {
            Airlines.Add(airline.Code, airline);
            return true;
        }
        return false;
    }

    public bool AddBoardingGate(BoardingGate gate)
    {
        if (!BoardingGates.ContainsKey(gate.GateName))
        {
            BoardingGates.Add(gate.GateName, gate);
            return true;
        }
        return false;
    }

    public void LoadFiles(string airlinesFile, string boardingGatesFile)
    {
        try
        {
            // Load airlines.csv
            foreach (var line in File.ReadAllLines(airlinesFile))
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    Airline airline = new Airline { Name = parts[0], Code = parts[1] };
                    AddAirline(airline);
                }
            }

            // Load boardinggates.csv
            foreach (var line in File.ReadAllLines(boardingGatesFile))
            {
                var parts = line.Split(',');
                if (parts.Length == 4)
                {
                    bool supportsCFFT = bool.TryParse(parts[1], out bool cfft);
                    bool supportsDDJB = bool.TryParse(parts[2], out bool ddjb);
                    bool supportsLWTT = bool.TryParse(parts[3], out bool lwtt);

                    if (supportsCFFT && supportsDDJB && supportsLWTT)
                    {
                        BoardingGate gate = new BoardingGate
                        {
                            GateName = parts[0],
                            SupportsCFFT = cfft,
                            SupportsDDJB = ddjb,
                            SupportsLWTT = lwtt
                        };
                        AddBoardingGate(gate);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading files: {ex.Message}");
        }
    }

    public Airline? GetAirlineFromFlight(Flight flight)
    {
        foreach (var airline in Airlines.Values)
        {
            if (airline.Flights.ContainsKey(flight.FlightNumber))
                return airline;
        }
        return null;
    }

    public void PrintAirlineFees()
    {
        foreach (var airline in Airlines.Values)
        {
            Console.WriteLine($"Airline: {airline.Name}, Fees: {airline.CalculateFees()}");
        }
    }
}

public class Airline
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public Dictionary<string, Flight> Flights { get; set; } = new();

    public bool AddFlight(Flight flight)
    {
        if (!Flights.ContainsKey(flight.FlightNumber))
        {
            Flights.Add(flight.FlightNumber, flight);
            return true;
        }
        return false;
    }

    public bool RemoveFlight(Flight flight)
    {
        return Flights.Remove(flight.FlightNumber);
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
}

public abstract class Flight
{
    public required string FlightNumber { get; set; }
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public required string Status { get; set; }
    public DateTime ExpectedTime { get; set; }

    public abstract double CalculateFees();
}

public class NORMFlight : Flight
{
    public override double CalculateFees() => 500.0;
}

public class CFFTFlight : Flight
{
    public double RequestFee { get; set; }
    public override double CalculateFees() => 1000.0 + RequestFee;
}

public class LWTTFlight : Flight
{
    public double RequestFee { get; set; }
    public override double CalculateFees() => 800.0 + RequestFee;
}

public class DDJBFlight : Flight
{
    public double RequestFee { get; set; }
    public override double CalculateFees() => 1200.0 + RequestFee;
}

public class BoardingGate
{
    public required string GateName { get; set; }
    public bool SupportsCFFT { get; set; }
    public bool SupportsDDJB { get; set; }
    public bool SupportsLWTT { get; set; }
    public Flight? Flight { get; set; }

    public double CalculateFees() => Flight?.CalculateFees() ?? 0.0;
}

