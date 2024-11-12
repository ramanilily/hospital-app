namespace CAB201HOSPAPP;
/// <summary>
/// Handles the assignment of surgeries to patients by floor managers.
/// </summary>
public class SurgeryAssignmentHandler
{
    private readonly IDataHandler _dataHandler;
    private int _surgeryAssignmentCount;
    
    /// <summary>
    /// Initializes a new instance of the SurgeryAssignmentHandler class.
    /// </summary>
    /// <param name="dataHandler">The data handler for accessing and updating data.</param>
    /// <param name="surgeryAssignmentCount">A reference to the surgery assignment sequence number.</param>
    public SurgeryAssignmentHandler(IDataHandler dataHandler, ref int surgeryAssignmentCount)
    {
        _dataHandler = dataHandler;
        _surgeryAssignmentCount = surgeryAssignmentCount;
    }
    
    /// <summary>
    /// Assigns a surgery to a patient by selecting a patient, surgeon, and surgery date.
    /// </summary>
    public void AssignSurgeryToPatient()
    {
        // Fetch registered patients via data handler
        var registeredPatients = _dataHandler.GetRegisteredPatients();
        
        if (registeredPatients.Count == 0)
        {
            Console.WriteLine("There are no registered patients.");
            return;
        }

        var patientsEligibleForSurgery = registeredPatients
            .Where(p => p.RoomNumber > 0 && p.AssignedSurgeon == null && !p.HasHadSurgery)
            .ToList();

        if (patientsEligibleForSurgery.Count == 0)
        {
            Console.WriteLine("There are no patients ready for surgery.");
            return;
        }

        var selectedPatient = SelectPatient(patientsEligibleForSurgery);
        var selectedSurgeon = SelectSurgeon();

        if (selectedPatient != null && selectedSurgeon != null)
        {
            var surgeryDateTime = RequestSurgeryDate();
            selectedPatient.AssignedSurgeon = selectedSurgeon;
            selectedPatient.surgeryDateTime = surgeryDateTime;

            _surgeryAssignmentCount++;
            selectedPatient.SurgeryAssignmentSequence = _surgeryAssignmentCount;

            Console.WriteLine(
                $"Surgeon {selectedSurgeon.Name} has been assigned to patient " +
                $"{selectedPatient.Name}.\nSurgery will take place on {surgeryDateTime:HH:mm dd/MM/yyyy}."
            );
        }
    }
    
    /// <summary>
    /// Allows the user to select a patient eligible for surgery.
    /// </summary>
    /// <param name="patientsEligibleForSurgery">List of patients eligible for surgery.</param>
    /// <returns>The selected patient.</returns>
    private Patient SelectPatient(List<Patient> patientsEligibleForSurgery)
    {
        Console.WriteLine("Please select your patient:");
        for (int i = 0; i < patientsEligibleForSurgery.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {patientsEligibleForSurgery[i].Name}");
        }

        while (true)
        {
            Console.WriteLine($"Please enter a choice between 1 and {patientsEligibleForSurgery.Count}.");
            if (int.TryParse(Console.ReadLine(), out int patientIndex) && patientIndex > 0 &&
                patientIndex <= patientsEligibleForSurgery.Count)
            {
                return patientsEligibleForSurgery[patientIndex - 1];
            }

            Console.WriteLine("#####\n#Error - Supplied value is out of range, please try again.\n#####");
        }
    }
    
    /// <summary>
    /// Allows the user to select a surgeon for the surgery.
    /// </summary>
    /// <returns>The selected surgeon.</returns>
    private Surgeon SelectSurgeon()
    {
        // Fetch registered users via data handler
        var registeredSurgeons = _dataHandler.GetRegisteredUsers().OfType<Surgeon>().ToList();

        if (registeredSurgeons.Count == 0)
        {
            Console.WriteLine("There are no registered surgeons.");
            return null;
        }

        Console.WriteLine("Please select your surgeon:");
        for (int i = 0; i < registeredSurgeons.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {registeredSurgeons[i].Name}");
        }

        while (true)
        {
            Console.WriteLine($"Please enter a choice between 1 and {registeredSurgeons.Count}.");
            if (int.TryParse(Console.ReadLine(), out int surgeonIndex) && surgeonIndex > 0 &&
                surgeonIndex <= registeredSurgeons.Count)
            {
                return registeredSurgeons[surgeonIndex - 1];
            }

            Console.WriteLine("#####\n#Error - Supplied value is out of range, please try again.\n#####");
        }
    }
    
    /// <summary>
    /// Prompts the user to enter a valid surgery date and time.
    /// </summary>
    /// <returns>The scheduled surgery date and time.</returns>
    private DateTime RequestSurgeryDate()
    {
        while (true)
        {
            Console.WriteLine("Please enter a date and time (e.g. 14:30 31/01/2024).");
            var dateTimeInput = Console.ReadLine();

            if (DateTime.TryParseExact(dateTimeInput, "HH:mm dd/MM/yyyy",
                    System.Globalization.CultureInfo.GetCultureInfo("en-GB"),
                    System.Globalization.DateTimeStyles.None,
                    out DateTime surgeryDateTime))
            {
                return surgeryDateTime;
            }

            Console.WriteLine("#####\n#Error - Supplied value is not a valid DateTime.\n#####");
        }
    }
}