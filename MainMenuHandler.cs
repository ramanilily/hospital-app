using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Channels;

namespace CAB201HOSPAPP;

/// <summary>
/// Handles the main menu of the application.
/// </summary>
public class MainMenuHandler
{
    private IDataHandler dataHandler;
    private UserRegistrationHandler registrationHandler;
    private LoginHandler loginHandler;

    public MainMenuHandler(IDataHandler dataHandler)
    {
        this.dataHandler = dataHandler;
        this.registrationHandler = new UserRegistrationHandler(dataHandler);
        this.loginHandler = new LoginHandler(dataHandler);
    }
    
    public static void DisplayMenuOptions(string[] options)
    {
        Console.WriteLine("Please choose from the menu below:");
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
    }
    
    /// <summary>
    /// Runs the main menu loop.
    /// </summary>
    public void Run()
    {
        bool showHeader = true;
        bool continueRunning = true;

        while (continueRunning)
        {
            if (showHeader)
            {
                DisplayHeader();
                showHeader = false;
            }

            continueRunning = DisplayMainMenu();
        }
    }

    private void DisplayHeader()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("Welcome to Gardens Point Hospital"); // main menu header
        Console.WriteLine("=================================");
    }

    private bool DisplayMainMenu()
    {
        string[] options = { "Login as a registered user", "Register as a new user", "Exit" }; //main menu options
        int choice = DisplayMenu("Please choose from the menu below:", options);
        
        switch (choice)
        {
            case 1:
                loginHandler.Login();
                return true;
            case 2:
                registrationHandler.RegisterUser();
                return true;
            case 3:
                Console.WriteLine("Goodbye. Please stay safe.");
                return false;
            default:
                Console.WriteLine("#####\n#Error - Invalid Menu Option, please try again.\n#####");
                return true;
        }
    }

    private int DisplayMenu(string menuTitle, string[] options)
    {
        while (true)
        {
            Console.WriteLine(menuTitle);
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            Console.WriteLine($"Please enter a choice between 1 and {options.Length}:");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= options.Length)
            {
                return choice;
            }
            else
            {
                Console.WriteLine("#####\n#Error - Invalid Menu Option, please try again.\n#####\n");
                // Loop continues, and the menu will be displayed again
            }
        }
    }
}

