namespace CAB201HOSPAPP;

/// <summary>
/// Handles the overall user registration process, allowing users to register as patients or staff.
/// </summary>
public class UserRegistrationHandler
{
    private readonly IDataHandler dataHandler;
    
    /// <summary>
    /// Initializes a new instance of the UserRegistrationHandler class.
    /// </summary>
    /// <param name="dataHandler">The data handler for accessing and updating user data.</param>
    public UserRegistrationHandler(IDataHandler dataHandler)
    {
        this.dataHandler = dataHandler;
    }
    
    /// <summary>
    /// Initiates the user registration process by prompting the user to select the type of user to register.
    /// </summary>
    public void RegisterUser()
    {
        Console.WriteLine("Register as which type of user:");
        string[] options = { "Patient", "Staff", "Return to the first menu" };

        // Display the menu options
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        Console.WriteLine("Please enter a choice between 1 and 3:");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int choice))
        {
            if (choice >= 1 && choice <= options.Length)
            {
                switch (choice)
                {
                    case 1:
                        var patientRegistrationHandler = new PatientRegistrationHandler(dataHandler);
                        patientRegistrationHandler.RegisterPatient();
                        break;
                    case 2:
                        var staffRegistrationHandler = new StaffRegistrationHandler(dataHandler);
                         staffRegistrationHandler.RegisterStaff();
                        break;
                    case 3:
                        // Return to the main menu
                        return;
                }
            }
            else
            {
                Console.WriteLine("#####\n#Error - Invalid Menu Option, please try again.\n#####");
                return;
            }
        }
        else
        {
            Console.WriteLine("#####\n#Error - Invalid Menu Option, please try again.\n#####");
            return;
        }
    }
}
