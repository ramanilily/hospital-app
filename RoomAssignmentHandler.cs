namespace CAB201HOSPAPP;

/// <summary>
/// Handles the assignment and unassignment of rooms to patients.
/// </summary>
public class RoomAssignmentHandler
{
    private readonly IDataHandler _dataHandler;
    private int floorNumber; 

    public RoomAssignmentHandler(IDataHandler dataHandler,  int floorNumber)
    {
        this._dataHandler = dataHandler;
        this.floorNumber = floorNumber;
    }

    public void AssignRoomToPatient()
    {
        var registeredPatients = _dataHandler.GetRegisteredPatients();
    
    if (registeredPatients.Count == 0)
    {
        Console.WriteLine("There are no registered patients.");
        return;
    }
    
    // Check if all rooms on the floor are assigned
    var assignedRoomsOnFloor = registeredPatients
        .Where(p => p.FloorNumber == floorNumber && p.RoomNumber != 0)
        .Select(p => p.RoomNumber)
        .Distinct()
        .ToList();

    if (assignedRoomsOnFloor.Count >= 10)
    {
        Console.WriteLine("#####\n#Error - All rooms on this floor are assigned.\n#####");
        return; // Return to the floor manager menu
    }

    // List only patients who are checked in and do not have an assigned room
    var patientsWithoutRoom = registeredPatients
        .Where(p => p.RoomNumber == 0 && p.isCheckedIn)
        .ToList();

    // Check if there are any checked-in patients without assigned rooms
    if (patientsWithoutRoom.Count == 0)
    {
        Console.WriteLine("There are no checked in patients.");
        return;
    }

    // Display the list of patients without assigned rooms
    Console.WriteLine("Please select your patient:");
    for (int i = 0; i < patientsWithoutRoom.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {patientsWithoutRoom[i].Name}");
    }

    // Let the user choose the patient to assign the room
    int patientChoice = GetValidChoice(1, patientsWithoutRoom.Count);
    Patient selectedPatient = patientsWithoutRoom[patientChoice - 1];

    // Prompt for the room number
    int roomNumber = GetValidRoomNumber(selectedPatient);

    // Assign the room and floor number to the patient
    selectedPatient.RoomNumber = roomNumber;
    selectedPatient.FloorNumber = floorNumber;

    // Display a confirmation message
    Console.WriteLine(
        $"Patient {selectedPatient.Name} has been assigned to room number {roomNumber} on floor {floorNumber}.");
}
    private int GetValidRoomNumber(Patient selectedPatient)
    {
        while (true)
        {
            Console.WriteLine("Please enter your room (1-10):");
            if (int.TryParse(Console.ReadLine(), out int roomNumber) && roomNumber >= 1 && roomNumber <= 10)
            {
                // Check if the room is already assigned to another patient on the same floor
                bool roomAlreadyAssigned = _dataHandler.GetRegisteredPatients().Any(p =>
                    p.RoomNumber == roomNumber &&
                    p.FloorNumber == floorNumber &&
                    p != selectedPatient);

                
                if (roomAlreadyAssigned)
                {
                    Console.WriteLine("#####\n#Error - Room has been assigned to another patient, please try again.\n#####");
                }
                else
                {
                    return roomNumber;
                }
            }
            else
            {
                Console.WriteLine("#####\n#Error - Supplied value is out of range, please try again.\n#####");
            }
        }
    }

    public void UnassignRoom()
    {
        var registeredPatients = _dataHandler.GetRegisteredPatients();
        var patientsWithRoomOnSameFloor = registeredPatients
            .Where(p => p.RoomNumber != 0 && !p.isCheckedIn && p.FloorNumber == floorNumber)
            .ToList();

        // Display the list of patients with assigned rooms on the same floor if available
        if (patientsWithRoomOnSameFloor.Count > 0)
        {
            Console.WriteLine("Please select your patient:");
            for (int i = 0; i < patientsWithRoomOnSameFloor.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {patientsWithRoomOnSameFloor[i].Name}");
            }

            // Let the user choose the patient to unassign the room
            int patientChoice = GetValidChoice(1, patientsWithRoomOnSameFloor.Count);
            Patient selectedPatient = patientsWithRoomOnSameFloor[patientChoice - 1];

            // Unassign the room from the selected patient
            int roomNumber = selectedPatient.RoomNumber;
            int floorNumber = selectedPatient.FloorNumber;

            // Reset the room and floor numbers
            selectedPatient.RoomNumber = 0;
            selectedPatient.FloorNumber = 0;

            // Display a confirmation message
            Console.WriteLine($"Room number {roomNumber} on floor {floorNumber} has been unassigned.");
        }
        else
        {
            // If no patients with assigned rooms on the same floor, print the message
            Console.WriteLine("There are no patients ready to have their rooms unassigned.");
        }
    }

    private int GetValidChoice(int min, int max)
    {
        int choice;
        while (true)
        {
            Console.WriteLine($"Please enter a choice between {min} and {max}:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out choice) && choice >= min && choice <= max)
            {
                return choice;
            }
            else
            {
                Console.WriteLine("#####\n#Error - Supplied value is out of range, please try again.\n#####");
            }
        }
    }
}


  
    

    
   