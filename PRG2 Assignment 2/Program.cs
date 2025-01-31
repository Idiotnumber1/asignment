//==========================================================
// Student Number : S10267191
// Student Name   : Bertrand Tang
// Partner Name   : Liu Yi Yang
//==========================================================

using System;
using System.Collections.Generic;
using System.IO;
using PRG2_Assignment_2;
using PRG2_Assignment_2.PRG2_Assignment_2;

class Program
{

    static void Main(string[] args)
    {
        //==========================================================================================
        //Main UI
        //==========================================================================================
        Terminal terminal = new Terminal("Changi Airport Terminal 5", new Dictionary<string, Airline>(), new Dictionary<string, BoardingGate>(), new Dictionary<string, double>());

        Console.WriteLine("=============================================");
        Console.WriteLine($"Welcome to {terminal.GetTerminalName()}");
        Console.WriteLine("=============================================");

        LoadAirlines("airlines.csv", terminal);
        LoadBoardingGates("boardinggates.csv", terminal);
        LoadFlights("flights.csv", terminal);

        while (true)
        {
            Console.WriteLine("1. List All Flights");
            Console.WriteLine("2. List Boarding Gates");
            Console.WriteLine("3. Assign a Boarding Gate to a Flight");
            Console.WriteLine("4. Create Flight");
            Console.WriteLine("5. Display Airline Flights");
            Console.WriteLine("6. Modify Flight Details");
            Console.WriteLine("7. Display Flight Schedule");
            Console.WriteLine("8. Process Unassigned Flights");
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
                    AssignFlight(terminal);
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
                case "8":
                    ProcessUnassignedFlights(terminal);
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            Console.WriteLine("\n\n=======================================================================");

        }
    }

    //==========================================================================================
    //List all flights
    //==========================================================================================
    static void ListAllFlights(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Flights for " + terminal.GetTerminalName());
        Console.WriteLine("=============================================");

        foreach (var airline in terminal.GetAirlines().Values)
        {
            foreach (var flight in airline.GetFlights().Values)
            {
                Console.WriteLine($"{flight.GetFlightNumber(),-10} {flight.GetOrigin(),-20} {flight.GetDestination(),-20} {flight.GetExpectedTime():dd/MM/yyyy hh:mm tt}");
            }
        }
        Console.WriteLine("\n\n=======================================================================");

    }

    //==========================================================================================
    //List Boarding Gates
    //==========================================================================================
    static void ListBoardingGates(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Boarding Gates for " + terminal.GetTerminalName());
        Console.WriteLine("=============================================");

        foreach (var gate in terminal.GetBoardingGates().Values)
        {
            Console.WriteLine($"{gate.GetGateName(),-10} CFFT: {gate.GetSupportsCFFT(),-5} DDJB: {gate.GetSupportsDDJB(),-5} LWTT: {gate.GetSupportsLWTT(),-5}");
        }
        Console.WriteLine("\n\n=======================================================================");

    }

    //==========================================================================================
    //Assign flights
    //==========================================================================================
    static void AssignFlight(Terminal terminal, string? flightNumber = null, string? gateName = null)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Assign a Boarding Gate to a Flight");
        Console.WriteLine("=============================================");

        if (string.IsNullOrEmpty(flightNumber))
        {
            Console.Write("Enter Flight Number: ");
            flightNumber = Console.ReadLine();
        }

        var flight = terminal.GetAirlines()
                             .Values
                             .SelectMany(a => a.GetFlights().Values)
                             .FirstOrDefault(f => f.GetFlightNumber() == flightNumber);

        if (flight == null)
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        if (string.IsNullOrEmpty(gateName))
        {
            Console.Write("Enter Boarding Gate Name: ");
            gateName = Console.ReadLine();
        }

        var gate = terminal.GetBoardingGates().Values.FirstOrDefault(g => g.GetGateName() == gateName);

        if (gate == null)
        {
            Console.WriteLine("Invalid gate name.");
            return;
        }

        if (gate.GetAssignedFlight() != null)
        {
            Console.WriteLine("This gate is already assigned to another flight.");
            return;
        }


        gate.SetAssignedFlight(flight);

        Console.WriteLine($"Flight {flight.GetFlightNumber()} has been successfully assigned to Gate {gate.GetGateName()}.");
    }





    //==========================================================================================
    //Create flight
    //==========================================================================================
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
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime expectedTime))
        {
            Console.WriteLine("Invalid date and time. Please try again.");
            return;
        }

        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        string? requestCode = Console.ReadLine();

        Console.Write("Enter Airline Code: ");
        string? airlineCode = Console.ReadLine();

        if (terminal.GetAirlines().TryGetValue(airlineCode!, out var airline))
        {
            Flight flight = requestCode switch
            {
                "CFFT" => new CFFTFlight(flightNumber!, origin!, destination!, "Scheduled", expectedTime, 0),
                "DDJB" => new DDJBFlight(flightNumber!, origin!, destination!, "Scheduled", expectedTime, 0),
                "LWTT" => new LWTTFlight(flightNumber!, origin!, destination!, "Scheduled", expectedTime, 0),
                _ => new NORMFlight(flightNumber!, origin!, destination!, "Scheduled", expectedTime)
            };
            airline.AddFlight(flight);
            Console.WriteLine($"Flight {flight.GetFlightNumber()} has been created.");
        }
        else
        {
            Console.WriteLine("Invalid airline code.");
        }
        Console.WriteLine("\n\n=======================================================================");

    }

    //==========================================================================================
    //display airline flight
    //==========================================================================================
    static void DisplayAirlineFlights(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for " + terminal.GetTerminalName());
        Console.WriteLine("=============================================");

        foreach (var airline in terminal.GetAirlines().Values)
        {
            Console.WriteLine($"{airline.GetCode()} - {airline.GetName()}");
        }

        Console.Write("Enter Airline Code: ");
        string? airlineCode = Console.ReadLine();

        if (terminal.GetAirlines().TryGetValue(airlineCode!, out var selectedAirline))
        {
            Console.WriteLine($"Flights for Airline: {selectedAirline.GetName()}");
            foreach (var flight in selectedAirline.GetFlights().Values)
            {
                Console.WriteLine($"{flight.GetFlightNumber()} - {flight.GetOrigin()} to {flight.GetDestination()}, Scheduled: {flight.GetExpectedTime():dd/MM/yyyy hh:mm tt}");
            }
        }
        else
        {
            Console.WriteLine("Invalid airline code.");
        }
        Console.WriteLine("\n\n=======================================================================");

    }

    //==========================================================================================
    //modify fligth details
    //==========================================================================================
    static void ModifyFlightDetails(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Modify Flight Details");
        Console.WriteLine("=============================================");

        Console.Write("Enter Flight Number: ");
        string? flightNumber = Console.ReadLine();

        var airline = terminal.GetAirlines()
                              .Values
                              .FirstOrDefault(a => a.GetFlights().ContainsKey(flightNumber!));

        if (airline == null)
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        Flight oldFlight = airline.GetFlights()[flightNumber!];

        Console.WriteLine("What would you like to modify?");
        Console.WriteLine("1. Origin");
        Console.WriteLine("2. Destination");
        Console.WriteLine("3. Expected Time");
        Console.WriteLine("4. Status");
        Console.Write("Select an option: ");
        string? option = Console.ReadLine();

        string newOrigin = oldFlight.GetOrigin();
        string newDestination = oldFlight.GetDestination();
        DateTime newTime = oldFlight.GetExpectedTime();
        string newStatus = oldFlight.GetStatus();

        switch (option)
        {
            case "1":
                Console.Write("Enter new Origin: ");
                newOrigin = Console.ReadLine()!;
                break;

            case "2":
                Console.Write("Enter new Destination: ");
                newDestination = Console.ReadLine()!;
                break;

            case "3":
                Console.Write("Enter new Expected Time (dd/MM/yyyy hh:mm): ");
                if (!DateTime.TryParse(Console.ReadLine(), out newTime))
                {
                    Console.WriteLine("Invalid date and time.");
                    return;
                }
                break;

            case "4":
                Console.Write("Enter new Status: ");
                newStatus = Console.ReadLine()!;
                break;

            default:
                Console.WriteLine("Invalid option.");
                return;
        }

        Flight newFlight = oldFlight switch
        {
            CFFTFlight => new CFFTFlight(flightNumber!, newOrigin, newDestination, newStatus, newTime, 0),
            DDJBFlight => new DDJBFlight(flightNumber!, newOrigin, newDestination, newStatus, newTime, 0),
            LWTTFlight => new LWTTFlight(flightNumber!, newOrigin, newDestination, newStatus, newTime, 0),
            _ => new NORMFlight(flightNumber!, newOrigin, newDestination, newStatus, newTime)
        };

        airline.RemoveFlight(oldFlight);
        airline.AddFlight(newFlight);

        Console.WriteLine("Flight details updated successfully!");
        Console.WriteLine("\n\n=======================================================================");

    }




    //==========================================================================================
    //display fligth schedule
    //==========================================================================================
    static void DisplayFlightSchedule(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Schedule");
        Console.WriteLine("=============================================");

        var allFlights = terminal.GetAirlines()
                                 .Values
                                 .SelectMany(a => a.GetFlights().Values)
                                 .OrderBy(f => f.GetExpectedTime()) 
                                 .ToList();

        if (allFlights.Count == 0)
        {
            Console.WriteLine("No scheduled flights for today.");
            return;
        }

        foreach (var flight in allFlights)
        {
            string airlineName = terminal.GetAirlines().Values
                                         .FirstOrDefault(a => a.GetFlights().ContainsKey(flight.GetFlightNumber()))?
                                         .GetName() ?? "Unknown Airline";
            string boardingGate = terminal.GetBoardingGates().Values
                                          .FirstOrDefault(g => g.GetAssignedFlight() == flight)?
                                          .GetGateName() ?? "None";
            string requestCode = flight is CFFTFlight ? "CFFT" :
                                 flight is DDJBFlight ? "DDJB" :
                                 flight is LWTTFlight ? "LWTT" : "None";

            Console.WriteLine($"{flight.GetFlightNumber()} | {airlineName} | {flight.GetOrigin()} -> {flight.GetDestination()}");
            Console.WriteLine($"Scheduled: {flight.GetExpectedTime():dd/MM/yyyy hh:mm tt} | Status: {flight.GetStatus()} | Special Request: {requestCode} | Gate: {boardingGate}");
            Console.WriteLine("-------------------------------------------------------------------");
        }
    }





//==========================================================================================
//ADV FEATS: Process unassigned flights
//==========================================================================================
static void ProcessUnassignedFlights(Terminal terminal)
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Processing Unassigned Flights");
        Console.WriteLine("=============================================");

        Queue<Flight> unassignedFlights = new Queue<Flight>();
        List<BoardingGate> unoccupiedGates = new List<BoardingGate>();

        foreach (var airline in terminal.GetAirlines().Values)
        {
            foreach (var flight in airline.GetFlights().Values)
            {
                bool isAssigned = terminal.GetBoardingGates().Values.Any(g => g.GetAssignedFlight() == flight);
                if (!isAssigned)
                {
                    unassignedFlights.Enqueue(flight);
                }
            }
        }

        foreach (var gate in terminal.GetBoardingGates().Values)
        {
            if (gate.GetAssignedFlight() == null)
            {
                unoccupiedGates.Add(gate);
            }
        }

        int initiallyUnassignedFlights = unassignedFlights.Count;
        int initiallyUnoccupiedGates = unoccupiedGates.Count;

        Console.WriteLine($"Total Unassigned Flights: {initiallyUnassignedFlights}");
        Console.WriteLine($"Total Unoccupied Boarding Gates: {initiallyUnoccupiedGates}");

        int assignedCount = 0;

        while (unassignedFlights.Count > 0 && unoccupiedGates.Count > 0)
        {
            Flight flight = unassignedFlights.Dequeue();
            BoardingGate? assignedGate = null;

            bool hasSpecialRequest = flight is CFFTFlight || flight is DDJBFlight || flight is LWTTFlight;

            if (hasSpecialRequest)
            {
                assignedGate = unoccupiedGates.FirstOrDefault(g =>
                    (flight is CFFTFlight && g.GetSupportsCFFT()) ||
                    (flight is DDJBFlight && g.GetSupportsDDJB()) ||
                    (flight is LWTTFlight && g.GetSupportsLWTT()));
            }

            if (assignedGate == null)
            {
                assignedGate = unoccupiedGates.FirstOrDefault();
            }

            if (assignedGate != null)
            {
                AssignFlight(terminal, flight.GetFlightNumber(), assignedGate.GetGateName());
                unoccupiedGates.Remove(assignedGate);
                assignedCount++;
            }
        }

        Console.WriteLine("\nSummary:");
        Console.WriteLine($"Flights Assigned Automatically: {assignedCount}");
        Console.WriteLine($"Remaining Unassigned Flights: {unassignedFlights.Count}");
        Console.WriteLine($"Remaining Unoccupied Gates: {unoccupiedGates.Count}");

        double percentageProcessed = (double)assignedCount / (initiallyUnassignedFlights + initiallyUnoccupiedGates) * 100;
        Console.WriteLine($"Total Percentage Processed Automatically: {percentageProcessed:F2}%");

        Console.WriteLine("=============================================");
    }







    //==========================================================================================
    //Loading stuff
    //==========================================================================================
    static void LoadAirlines(string airlinesFile, Terminal terminal)
    {
        if (!File.Exists(airlinesFile))
        {
            Console.WriteLine($"Error: File {airlinesFile} not found.");
            return;
        }
        else {
            Console.WriteLine("Loaded Airlines");
             }

        foreach (var line in File.ReadAllLines(airlinesFile))
        {
            var parts = line.Split(',');
            if (parts.Length == 2)
            {
                var airline = new Airline(parts[0].Trim(), parts[1].Trim());
                terminal.GetAirlines().Add(airline.GetCode(), airline);
            }
        }
    }

    static void LoadBoardingGates(string boardingGatesFile, Terminal terminal)
    {
        if (!File.Exists(boardingGatesFile))
        {
            Console.WriteLine($"Error: File {boardingGatesFile} not found.");
            return;
        }
        else
        {
            Console.WriteLine("Loaded Gates");
        }

        foreach (var line in File.ReadAllLines(boardingGatesFile))
        {
            var parts = line.Split(',');
            if (parts.Length == 4)
            {
                bool.TryParse(parts[1], out bool supportsCFFT);
                bool.TryParse(parts[2], out bool supportsDDJB);
                bool.TryParse(parts[3], out bool supportsLWTT);

                var gate = new BoardingGate(parts[0].Trim(), supportsCFFT, supportsDDJB, supportsLWTT);
                terminal.GetBoardingGates().Add(gate.GetGateName(), gate);
            }
        }
    }

    static void LoadFlights(string flightsFile, Terminal terminal)
    {
        if (!File.Exists(flightsFile))
        {
            Console.WriteLine($"Error: File {flightsFile} not found.");
            return;
        }
        else
        {
            Console.WriteLine("Loaded Flights");
        }

        foreach (var line in File.ReadAllLines(flightsFile))
        {
            var parts = line.Split(',');
            if (parts.Length >= 5)
            {
                string flightNumber = parts[0].Trim();
                string origin = parts[1].Trim();
                string destination = parts[2].Trim();

                if (!DateTime.TryParseExact(parts[3].Trim(), "h:mm tt", null, System.Globalization.DateTimeStyles.None, out DateTime expectedTime))
                {
                    if (flightNumber == "Flight Number") { continue; }
                    else
                    {
                        Console.WriteLine($"Warning: Invalid time format for flight {flightNumber}.");
                        continue;
                    }
                }

                string requestCode = parts[4].Trim();
                Flight flight = requestCode switch
                {
                    "CFFT" => new CFFTFlight(flightNumber, origin, destination, "Scheduled", expectedTime, 0),
                    "DDJB" => new DDJBFlight(flightNumber, origin, destination, "Scheduled", expectedTime, 0),
                    "LWTT" => new LWTTFlight(flightNumber, origin, destination, "Scheduled", expectedTime, 0),
                    _ => new NORMFlight(flightNumber, origin, destination, "Scheduled", expectedTime)
                };

                string airlineCode = flightNumber.Substring(0, 2);
                if (terminal.GetAirlines().TryGetValue(airlineCode, out var airline))
                {
                    airline.AddFlight(flight);
                }
                else
                {
                    Console.WriteLine($"Warning: Airline code {airlineCode} for flight {flightNumber} does not exist.");
                }
            }
        }
    }
}
