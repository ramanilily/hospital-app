using System.Threading.Channels;

namespace CAB201HOSPAPP;

/// <summary>
/// Handles the registration process for staff members, including floor managers and surgeons.
/// Inherits from BaseRegistrationHandler.
/// </summary>
public class StaffRegistrationHandler : BaseRegistrationHandler
{
    private const int MinStaffId = 100;
    private const int MaxStaffId = 999;
    private readonly IDataHandler dataHandler;
    
    /// <summary>
    /// Initializes a new instance of the StaffRegistrationHandler class.
    /// </summary>
    /// <param name="dataHandler">The data handler for accessing and updating user data.</param>
    public StaffRegistrationHandler(IDataHandler dataHandler)
    {
        this.dataHandler = dataHandler;
    }
    
    /// <summary>
    /// Prompts the user to enter a valid, unique staff ID within the allowed range.
    /// </summary>
    /// <returns>A valid staff ID.</returns>
    public int GetValidStaffId()
    {
        while (true)
        {
            Console.WriteLine("Please enter in your staff ID:");
            string input = Console.ReadLine().Trim();

            if (int.TryParse(input, out int staffId) && staffId >= MinStaffId && staffId <= MaxStaffId)
            {
                if (dataHandler.CheckIfStaffIdExists(staffId))
                {
                    Console.WriteLine("#####\n#Error - Staff ID is already registered, please try again.\n#####");
                }
                else
                {
                    return staffId;
                }
            }
            else
            {
                Console.WriteLine("#####\n#Error - Supplied staff identification number is invalid, please try again.\n#####");
            }
        }
    }
    
    /// <summary>
    /// Initiates the staff registration process by allowing the user to choose the type of staff to register.
    /// </summary>
    public virtual void RegisterStaff()
    {
        string[] staffOptions = { "Floor manager", "Surgeon", "Return to the first menu" };

        while (true)
        {
            int choice = UserInputHandler.DisplayMenuAndGetChoice(staffOptions, "Register as which type of staff:", "Please enter a choice between 1 and 3:");

            if (choice == 3) return;

            switch (choice)
            {
                case 1:
                    var floorManagerRegistrationHandler = new FloorManagerRegistrationHandler(dataHandler);
                    floorManagerRegistrationHandler.RegisterFloorManager();
                    return;
                case 2:
                    var surgeonRegistrationHandler = new SurgeonRegistrationHandler(dataHandler);
                    surgeonRegistrationHandler.RegisterSurgeon();
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}


