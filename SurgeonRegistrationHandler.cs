namespace CAB201HOSPAPP;
/// <summary>
/// Handles the registration process for surgeons.
/// Inherits from StaffRegistrationHandler.
/// </summary>
public class SurgeonRegistrationHandler : StaffRegistrationHandler
{
    private const int MAX_SURGEON_AGE = 75;
    private const int MIN_SURGEON_AGE = 30;
    private readonly IDataHandler dataHandler;
    
    /// <summary>
    /// Initializes a new instance of the SurgeonRegistrationHandler class.
    /// </summary>
    /// <param name="dataHandler">The data handler for accessing and updating user data.</param>
    public SurgeonRegistrationHandler(IDataHandler dataHandler) : base (dataHandler)
    {
        this.dataHandler = dataHandler;
    }
    /// <summary>
    /// Registers a new surgeon by collecting necessary details.
    /// </summary>
    public void RegisterSurgeon()
    {
        Console.WriteLine("Registering as a surgeon.");
        Surgeon newSurgeon = new Surgeon();
        
        CollectBasicDetails(newSurgeon, dataHandler.GetRegisteredUsers(), MIN_SURGEON_AGE, MAX_SURGEON_AGE);

        // Request staff ID and ensure it's valid and unregistered
        newSurgeon.Staffid = GetValidStaffId();
        // Select specialty
        newSurgeon.Speciality = SelectSpeciality();

        // Add the new surgeon to the registered users
        dataHandler.RegisterUser(newSurgeon);
        Console.WriteLine($"{newSurgeon.Name} is registered as a surgeon.");
    }
    
    /// <summary>
    /// Allows the user to select a specialty for the surgeon.
    /// </summary>
    /// <returns>The selected speciality as a string.</returns>
    private string SelectSpeciality()
    {
        var specialties = new Dictionary<string, string>
        {
            { "1", "General Surgeon" },
            { "2", "Orthopaedic Surgeon" },
            { "3", "Cardiothoracic Surgeon" },
            { "4", "Neurosurgeon" }
        };
        string input;
        do
        {
            Console.WriteLine("Please choose your speciality:");
            foreach (var speciality in specialties)
            {
                Console.WriteLine($"{speciality.Key}. {speciality.Value}");
            }
            Console.WriteLine("Please enter a choice between 1 and 4.");

            input = Console.ReadLine().Trim();

            if (!specialties.ContainsKey(input))
            {
                Console.WriteLine("#####\n#Error - Non-valid speciality type, please try again.\n#####");
            }
        } while (!specialties.ContainsKey(input));

        return specialties[input];
        
    }
}