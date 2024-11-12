namespace CAB201HOSPAPP;
/// <summary>
/// Handles the menu interactions and actions for a surgeon.
/// </summary>
public class SurgeonMenuHandler
{
    private Surgeon surgeon;
    private List<Patient> registeredPatients;
    private IDataHandler dataHandler;

    /// <summary>
    /// Initializes a new instance of the SurgeonMenuHandler class.
    /// </summary>
    /// <param name="surgeon">The surgeon using the menu.</param>
    /// <param name="patients">The list of registered patients.</param>
    /// <param name="dataHandler">The data handler for accessing and updating data.</param>
    public SurgeonMenuHandler(Surgeon surgeon, List<Patient> patients, IDataHandler dataHandler)
    {
        this.surgeon = surgeon;
        this.registeredPatients = patients;
        this.dataHandler = dataHandler;
    }

    /// <summary>
    /// Displays the surgeon menu and processes user input.
    /// </summary>
    public void DisplayMenu()
    {
        bool exitMenu = false;
        while (!exitMenu)
        {
            string[] options =
            {
                "Display my details", "Change password", "See your list of patients", "See your schedule",
                "Perform surgery", "Log out"
            }; 
            
            //patient menu options
            int choice = UserInputHandler.DisplayMenuAndGetChoice(options, "Surgeon Menu.", "Please choose from the menu below:", printPromptonNewLine: false);
            exitMenu = HandleMenuChoice(choice);
        }
    }
    
    /// <summary>
    /// Executes the action based on the surgeon's menu choice.
    /// </summary>
    /// <param name="choice">The surgeon's menu choice.</param>
    /// <returns>True if the surgeon chooses to log out; otherwise, false.</returns>
    private bool HandleMenuChoice(int choice)
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
                SeePatients();
                return false;
            case 4:
                SeeSchedule();
                return false;
            case 5:
                PerformSurgery();
                return false;
            case 6:
                Console.WriteLine($"Surgeon {surgeon.Name} has logged out.");
                return true;
            default:
                Console.WriteLine("Invalid choice");
                return true;
        }
    }
    
    /// <summary>
    /// Displays the surgeon's personal details.
    /// </summary>
    private void DisplayDetails()
        {
            Console.WriteLine("Your details."); 
            Console.WriteLine($"Name: {surgeon.Name}");
            Console.WriteLine($"Age: {surgeon.Age}");
            Console.WriteLine($"Mobile phone: {surgeon.MobileNumber}");
            Console.WriteLine($"Email: {surgeon.Email}");
            Console.WriteLine($"Staff ID: {surgeon.Staffid}");
            Console.WriteLine($"Speciality:{surgeon.Speciality}");
        }

    /// <summary>
    /// Allows the surgeon to change their password.
    /// </summary>
    private void ChangePassword()
    {
        Console.WriteLine("Enter new password:");
        string? newPassword = Console.ReadLine();
        if (!string.IsNullOrEmpty(newPassword))
        {
            surgeon.Password = newPassword;
            Console.WriteLine("Password has been changed.");
        }
    }
    
    /// <summary>
    /// Displays the list of patients assigned to the surgeon.
    /// </summary>
    private void SeePatients()
    {
         var surgeonPatients = GetSurgeonPatients()
            .Where(p => p.surgeryDateTime != null && !p.HasHadSurgery)
            .OrderBy(p => p.SurgeryAssignmentSequence)
            .ToList();
                    
        Console.WriteLine("Your Patients.");
        if (surgeonPatients.Count == 0)
        {
            Console.WriteLine("You do not have any patients assigned.");
        }
        else
        {
            for (int i = 0; i < surgeonPatients.Count; i++)
            {
                Console.WriteLine($"{i+1}. {surgeonPatients[i].Name}");
            }
        }
    }
    
    /// <summary>
    /// Displays the surgeon's surgery schedule.
    /// </summary>
    private void SeeSchedule()
    {
        var scheduledSurgeries = registeredPatients
            .Where(p => p.AssignedSurgeon == surgeon && p.surgeryDateTime != null)
            .OrderBy(p=> p.surgeryDateTime)
            .ToList();

        Console.WriteLine("Your schedule.");
        if (scheduledSurgeries.Count == 0)
        {
            Console.WriteLine("You do not have any patients assigned.");
        }
        else
        {
            foreach (var surgery in scheduledSurgeries)
            {
                Console.WriteLine($"Performing surgery on patient {surgery.Name} on {surgery.surgeryDateTime.Value:HH:mm dd/MM/yyyy}");
            }
        }
    }
    
    /// <summary>
    /// Allows the surgeon to perform surgery on an assigned patient.
    /// </summary>
    private void PerformSurgery()
    {
         var patientsForSurgery = GetSurgeonPatients()
            .Where(p => p.surgeryDateTime != null && !p.HasHadSurgery)
            .OrderBy(p => p.SurgeryAssignmentSequence)
            .ToList();

        if (patientsForSurgery.Count == 0)
        {
            Console.WriteLine("You have no patients assigned for surgery.");
        }
        else
        {
            Console.WriteLine("Please select your patient:");
            for (int i = 0; i < patientsForSurgery.Count; i++)
            { 
                Console.WriteLine($"{i+1}. {patientsForSurgery[i].Name}");
            }

            int patientIndex = -1;
            bool validPatientSelection = false;
            while (!validPatientSelection)
            { 
                Console.WriteLine($"Please enter a choice between 1 and {patientsForSurgery.Count}.");
                string input = Console.ReadLine();

                if (int.TryParse(input, out patientIndex) && 
                    patientIndex > 0 && patientIndex <= patientsForSurgery.Count)
                { 
                    validPatientSelection = true;
                }
                else
                { 
                    Console.WriteLine("#####\n#Error - Supplied value is out of range, please try again.\n#####");
                } }
                        
            var selectedPatient = patientsForSurgery[patientIndex - 1];
            Console.WriteLine($"Surgery performed on {selectedPatient.Name} by {surgeon.Name}.");
            selectedPatient.HasHadSurgery = true;
        }
    }
    /// <summary>
    /// Retrieves the list of patients assigned to the surgeon.
    /// </summary>
    /// <returns>A list of patients assigned to the surgeon.</returns>
    private List<Patient> GetSurgeonPatients()
    {
        return registeredPatients.Where(p => p.AssignedSurgeon == surgeon).ToList();
    }
}
