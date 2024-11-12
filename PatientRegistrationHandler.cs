namespace CAB201HOSPAPP;

/// <summary>
/// Handles the registration process for patients.
/// </summary>
public class PatientRegistrationHandler : BaseRegistrationHandler
{
        private const int MaxPatientAge = 100;
        private const int MinPatientAge = 0;
        private readonly IDataHandler dataHandler;
   

    public PatientRegistrationHandler(IDataHandler dataHandler)
    {
        this.dataHandler = dataHandler;
    }
    
    /// <summary>
    /// Registers a new patient by collecting basic details.
    /// </summary>
    public void RegisterPatient()
    {
        Console.WriteLine("Registering as a patient.");
        
        Patient newPatient = new Patient();
        
        CollectBasicDetails(newPatient, dataHandler.GetRegisteredUsers(),MinPatientAge, MaxPatientAge);
        
        dataHandler.RegisterUser(newPatient);
        Console.WriteLine($"{newPatient.Name} is registered as a patient.");
    }
}