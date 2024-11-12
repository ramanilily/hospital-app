using System.Threading.Channels;

namespace CAB201HOSPAPP;
/// <summary>
/// Handles the menu interactions and actions for a floor manager.
/// </summary>
public class FloorManagerMenuHandler
{
    private readonly IDataHandler _dataHandler;
    private int SurgeryAssignmentCount;
    private FloorManager floorManager;
    private RoomAssignmentHandler roomAssignmentHandler;
    
    /// <summary>
    /// Initializes a new instance of the FloorManagerMenuHandler class.
    /// </summary>
    /// <param name="floorManager">The floor manager using the menu.</param>
    /// <param name="dataHandler">The data handler for accessing data.</param>

    public FloorManagerMenuHandler(FloorManager floorManager, IDataHandler dataHandler)
    {
        this.floorManager = floorManager;
        this._dataHandler = dataHandler;

        // Initialize room assignment handler with patients directly from dataHandler
        this.roomAssignmentHandler = new RoomAssignmentHandler(_dataHandler, floorManager.FloorNumber);
    }
    
    /// <summary>
    /// Displays the floor manager menu and processes user input.
    /// </summary>
    public void DisplayMenu()
    {
        bool exitMenu = false;
        while (!exitMenu)
        {
            string[] options =
            {
                "Display my details", "Change password", "Assign room to patient", "Assign surgery",
                "Unassign room", "Log out"
            };

            int choice = UserInputHandler.DisplayMenuAndGetChoice(
                options, 
                "Floor Manager Menu.", 
                "Please choose from the menu below:", 
                printPromptonNewLine: false
            );
            exitMenu = PerformAction(choice);
        }
    }
    
    /// <summary>
    /// Executes the action based on the user's menu choice.
    /// </summary>
    /// <param name="choice">The user's menu choice.</param>
    /// <returns>True if the user chooses to log out; otherwise, false.</returns>
    private bool PerformAction(int choice)
    {
        switch (choice)
        {
            case 1:
                DisplayDetails();
                return false;
            case 2:
                ChangePassword();
                return false;
            case 3:
                AssignRoomToPatient();
                return false;
            case 4:
                AssignSurgeryToPatient();
                return false;
            case 5:
                UnassignRoom();
                return false;
            case 6:
                return LogOut();
            default:
                Console.WriteLine("#####\n#Error - Invalid Menu Option, please try again.\n#####");
                return false;
        }
    }

    private void AssignRoomToPatient()
    {
        // Assign room to patient using the handler
        roomAssignmentHandler.AssignRoomToPatient();
    }

    private void AssignSurgeryToPatient()
    {
        SurgeryAssignmentHandler surgeryAssignmentHandler = new SurgeryAssignmentHandler(_dataHandler, ref SurgeryAssignmentCount);
        surgeryAssignmentHandler.AssignSurgeryToPatient();
    }

    private void UnassignRoom()
    {
        roomAssignmentHandler.UnassignRoom();
    }

    private void DisplayDetails()
    {
        Console.WriteLine("Your details.");
        Console.WriteLine($"Name: {floorManager.Name}");
        Console.WriteLine($"Age: {floorManager.Age}");
        Console.WriteLine($"Mobile phone: {floorManager.MobileNumber}");
        Console.WriteLine($"Email: {floorManager.Email}");
        Console.WriteLine($"Staff ID: {floorManager.Staffid}");
        Console.WriteLine($"Floor: {floorManager.FloorNumber}.");
    }

    private void ChangePassword()
    {
        Console.WriteLine("Enter new password:");
        string? newPassword = Console.ReadLine();
        if (!string.IsNullOrEmpty(newPassword))
        {
            floorManager.Password = newPassword;
            Console.WriteLine("Password has been changed.");
            
            // Update dataHandler to reflect the password change
            _dataHandler.UpdateUser(floorManager);
        }
    }

    private bool LogOut()
    {
        Console.WriteLine($"Floor manager {floorManager.Name} has logged out.");
        return true;
    }
}