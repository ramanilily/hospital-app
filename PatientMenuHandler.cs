namespace CAB201HOSPAPP;

/// <summary>
/// Handles the menu interactions and actions for a patient.
/// </summary>
public class PatientMenuHandler
{
    private readonly Patient _patient;
    private readonly IDataHandler _dataHandler;

    public PatientMenuHandler(Patient patient, IDataHandler dataHandler)
    {
        _patient = patient;
        _dataHandler = dataHandler;
    }

    public void DisplayMenu()
    {
        bool exitMenu = false;
        while (!exitMenu)
        {
            string checkInOption = _patient.isCheckedIn ? "Check out" : "Check in";

            string[] options =
            {
                "Display my details",
                "Change password",
                checkInOption,
                "See room",
                "See surgeon",
                "See surgery date and time",
                "Log out"
            };

            // Call the method without passing a title as it's already printed
            int choice = UserInputHandler.DisplayMenuAndGetChoice(options, "Patient Menu.", "Please choose from the menu below:", printPromptonNewLine: false);
            exitMenu = PerformAction(choice);
        }
    }

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
                ToggleCheckInStatus();
                return false;
            case 4:
                SeeRoom();
                return false;
            case 5:
                SeeSurgeon();
                return false;
            case 6:
                SeeSurgeryTime();
                return false;
            case 7:
                return LogOut();
            default:
                Console.WriteLine("#####\n#Error - Invalid Menu Option, please try again.\n#####");
                return false;
        }
    }
    
    private void DisplayDetails()
    {
        Console.WriteLine("Your details."); 
        Console.WriteLine($"Name: {_patient.Name}");
        Console.WriteLine($"Age: {_patient.Age}");
        Console.WriteLine($"Mobile phone: {_patient.MobileNumber}");
        Console.WriteLine($"Email: {_patient.Email}");
    }
    
    private void ChangePassword()
    {
        Console.WriteLine("Enter new password:");
        string? newPassword = Console.ReadLine();
        if (!string.IsNullOrEmpty(newPassword))
        { 
            _patient.Password = newPassword; 
            _dataHandler.UpdateUser(_patient);
            Console.WriteLine("Password has been changed.");
        }
    }

    private void ToggleCheckInStatus()
    {
        if (_patient.isCheckedIn)
        {
            if (_patient.HasHadSurgery)
            {
                Console.WriteLine($"Patient {_patient.Name} has been checked out.");
                _patient.isCheckedIn = false;
                _dataHandler.UpdateUser(_patient);
            }
            else
            {
                Console.WriteLine("You are unable to check out at this time.");
            }
        }
        else
        {
            if (_patient.HasHadSurgery)
            {
                Console.WriteLine("You are unable to check in at this time.");
            }
            else
            {
                Console.WriteLine($"Patient {_patient.Name} has been checked in.");
                _patient.isCheckedIn = true;
                _dataHandler.UpdateUser(_patient);
            }
        }
    }

    private void SeeRoom()
    {
        if (_patient.RoomNumber > 0)
        {
            Console.WriteLine($"Your room is number {_patient.RoomNumber} on floor {_patient.FloorNumber}."); 
        }
        else
        {
            Console.WriteLine("You do not have an assigned room.");
        }
    }

    private void SeeSurgeon()
    {
        if (_patient.AssignedSurgeon != null)
        {
            Console.WriteLine($"Your surgeon is {_patient.AssignedSurgeon.Name}."); 
        }
        else
        {
            Console.WriteLine("You do not have an assigned surgeon.");
        }
    }

    private void SeeSurgeryTime()
    {
        if (_patient.surgeryDateTime != null)
        {
            Console.WriteLine($"Your surgery time is {_patient.surgeryDateTime.Value: HH:mm dd/MM/yyyy}.");
        }
        else
        {
            Console.WriteLine("You do not have assigned surgery. ");
        }
    }

    private bool LogOut()
    {
        Console.WriteLine($"Patient {_patient.Name} has logged out.");
        return true;
    }
}