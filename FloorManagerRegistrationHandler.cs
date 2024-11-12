namespace CAB201HOSPAPP;
/// <summary>
/// Handles the registration process for floor managers, including floor assignment.
/// Inherits from StaffRegistrationHandler.
/// </summary>

public class FloorManagerRegistrationHandler : StaffRegistrationHandler
{
    private const int MaxFloorManagerAge = 70;
    private const int MinFloorManagerAge = 21;
    private readonly IDataHandler dataHandler;
    private static HashSet<int> assignedFloors = new HashSet<int>();

    public FloorManagerRegistrationHandler(IDataHandler dataHandler) : base (dataHandler)
    {
        this.dataHandler = dataHandler;
    }
    
    /// <summary>
    /// Registers a new floor manager and assigns them to a floor.
    /// </summary>
    public void RegisterFloorManager()
    {
        FloorManager newFloorManager = new FloorManager();
        
        if (assignedFloors.Count >= 6)
        {
            Console.WriteLine("#####\n#Error - All floors are assigned.\n#####");
            return; // Exit the registration process
        }
        Console.WriteLine("Registering as a floor manager.");
        
        CollectBasicDetails(newFloorManager, dataHandler.GetRegisteredUsers(), MinFloorManagerAge, MaxFloorManagerAge);

        // Request staff ID and ensure it's valid and unregistered
        newFloorManager.Staffid = GetValidStaffId();
        
        // Request floor number
        newFloorManager.FloorNumber = GetValidFloorNumber();
        assignedFloors.Add(newFloorManager.FloorNumber);

        // Add the new floor manager to the registered users
        dataHandler.RegisterUser(newFloorManager);
        Console.WriteLine($"{newFloorManager.Name} is registered as a floor manager.");
    }
    

    private int GetValidFloorNumber()
    {
        int floorNumber;
        while (true)
        {
            Console.WriteLine("Please enter in your floor number:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out floorNumber) && floorNumber >= 1 && floorNumber <= 6)
            {
                if (assignedFloors.Contains(floorNumber))
                {
                    Console.WriteLine(
                        "#####\n#Error - Floor has been assigned to another floor manager, please try again.\n#####");
                }
                else
                {
                    return floorNumber;
                }
            }
            else
            {
                Console.WriteLine("#####\n#Error - Supplied floor is invalid, please try again.\n#####");
            }
        }
    }
}
    
    

