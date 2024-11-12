namespace CAB201HOSPAPP;

/// <summary>
/// Handles the login process for users.
/// </summary>
public class LoginHandler
{
    private IDataHandler dataHandler;

    public LoginHandler(IDataHandler dataHandler)
    {
        this.dataHandler = dataHandler;
    }
    
    /// <summary>
    /// Prompts the user to log in and navigates to the appropriate menu upon successful authentication.
    /// </summary>
    public void Login()
    {
        Console.WriteLine("Login Menu.");
        var registeredUsers = dataHandler.GetRegisteredUsers();
        if (!registeredUsers.Any())
        {
            Console.WriteLine("#####\n#Error - There are no people registered.\n#####");
            return;
        }

        Console.WriteLine("Please enter in your email:");
        string? email = Console.ReadLine();
        
        // Find registered user
        User? foundUser =
            registeredUsers.Find(user =>
                user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (foundUser == null)
        {
            Console.WriteLine("#####\n#Error - Email is not registered.\n#####");
            return;
        }

        Console.WriteLine("Please enter in your password:");
        string? password = Console.ReadLine();

        if (foundUser.Password == password)
        {
            Console.WriteLine($"Hello {foundUser.Name} welcome back.");
            NavigateToUserSpecificMenu(foundUser);
        }
        else
        {
            Console.WriteLine("#####\n#Error - Wrong Password.\n#####");
        }
    }

    private void NavigateToUserSpecificMenu(User user)
        {
            if (user is FloorManager floorManager)
            {
                new FloorManagerMenuHandler(floorManager, dataHandler).DisplayMenu();
            }
            else if (user is Surgeon surgeon)
            {
                var registeredPatients = dataHandler.GetRegisteredPatients();
                new SurgeonMenuHandler( surgeon, registeredPatients, dataHandler).DisplayMenu();
            }
            else if (user is Patient patient)
            {
                new PatientMenuHandler(patient, dataHandler).DisplayMenu();
            }
        }
    }

        


       