using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CAB201HOSPAPP;

/// <summary>
/// Provides methods for handling and validating user input.
/// </summary>
    public static class UserInputHandler
    {
        /// <summary>
        /// Requests and validates a user's name.
        /// </summary>
        /// <returns>A valid name string.</return
        public static string RequestValidName()
        { ;
            while (true)
            {
                Console.WriteLine("Please enter in your name:");
                string? nameInput = Console.ReadLine()?.Trim();
                
                // Check if the name is not empty and contains only alphabetic characters and spaces
                if (!string.IsNullOrEmpty(nameInput) && Regex.IsMatch(nameInput, "^[A-Za-z\\s]+$"))
                {
                    return nameInput.Trim();
                   
                }
                else
                {
                    Console.WriteLine("#####\n#Error - Supplied name is invalid, please try again.\n#####");
                }
            }
        }

        /// <summary>
        /// Requests and validates a user's age within a specified range.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="minAge">The minimum acceptable age.</param>
        /// <param name="maxAge">The maximum acceptable age.</param>
        /// <returns>A valid age integer.</returns>
        public static int RequestValidAge(string prompt, int minAge, int maxAge)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine().Trim();

                // First, check if the input is an integer
                if (int.TryParse(input, out int age))
                {
                    // Then, check if the age is within the valid range
                    if (age >= minAge && age <= maxAge)
                    {
                        return age;
                    }
                    else
                    {
                        Console.WriteLine("#####\n#Error - Supplied age is invalid, please try again.\n#####");
                    }
                }
                else
                {
                    Console.WriteLine("#####\n#Error - Supplied value is not an integer, please try again.\n#####");
                }
            }
        }

        /// <summary>
        /// Requests and validates a user's mobile number.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <returns>A valid mobile number string.</returns>
        public static string RequestValidMobileNumber()
        {
            while (true)
            {
                Console.WriteLine("Please enter in your mobile number:");
                string mobileInput = Console.ReadLine().Trim();

                if (Regex.IsMatch(mobileInput, @"^0\d{9}$"))
                {
                    return mobileInput;
                }
                else
                {
                    Console.WriteLine("#####\n#Error - Supplied mobile number is invalid, please try again.\n#####");
                }
            }
        
        }

        /// <summary>
        /// Requests and validates a user's email address, ensuring it's not already registered.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="registeredUsers">List of already registered users.</param>
        /// <returns>A valid email string.</returns>
        public static string RequestValidEmail(List<User> registeredUsers)
        {
            while (true)
            {
                Console.WriteLine("Please enter in your email:");
                string? emailInput = Console.ReadLine();


                // Validate email format and check if it already exists in the registered users list
                if (!string.IsNullOrEmpty(emailInput) &&
                    emailInput.Contains("@") &&
                    emailInput.IndexOf("@") > 0 && // ensure @ is not the first character
                    emailInput.IndexOf("@") < emailInput.Length - 1) // ensure @ is not the last character
                {
                    if (registeredUsers.Any(user => user.Email.Equals(emailInput, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine("#####\n#Error - Email is already registered, please try again.\n#####");
                    }
                    else
                    {
                        return emailInput;

                    }
                }
                else
                {
                    Console.WriteLine("#####\n#Error - Supplied email is invalid, please try again.\n#####");
                }

            }
        }
        
        /// <summary>
        /// Requests and validates a user's password.
        /// </summary>
        /// <returns>A valid password string.</returns>
        public static string RequestValidPassword()
        {
            while (true)
            {
                Console.WriteLine("Please enter in your password:");
                string? passwordInput = Console.ReadLine();
                
                // Ensure password is valid with the correct length and complexity
                if (!string.IsNullOrEmpty(passwordInput) &&
                    passwordInput.Length >= 8 &&
                    passwordInput.Any(char.IsDigit) &&
                    passwordInput.Any(char.IsLower) &&
                    passwordInput.Any(char.IsUpper))
                {
                    return passwordInput;
                }
                else
                {
                    Console.WriteLine("#####\n#Error - Supplied password is invalid, please try again.\n#####");
                }
            }
        }

        /// <summary>
        /// Displays a menu and gets a valid choice from the user.
        /// </summary>
        /// <param name="options">An array of menu options to display.</param>
        /// <param name="menuTitle">The header to display before the menu.</param>
        /// <param name="prompt">The prompt for the user's choice.</param>
        /// <returns>A valid menu choice integer.</returns>
        public static int DisplayMenuAndGetChoice(string[] options, string menuTitle = "", string prompt = "", bool printPromptonNewLine = true)
        {
            while (true)
            {
                // If a menu title is provided, print it
                if (!string.IsNullOrWhiteSpace(menuTitle))
                {
                    Console.WriteLine(menuTitle);
                }

                if (!string.IsNullOrEmpty(prompt) && !printPromptonNewLine)
                {
                    Console.WriteLine(prompt);
                }

                // Display the menu options
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }
              
                
                // If a prompt is provided, use it; otherwise, use the default prompt.
                string finalPrompt = $"Please enter a choice between 1 and {options.Length}:";
                Console.WriteLine(finalPrompt);
                
                
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int numChoice) && numChoice >= 1 && numChoice <= options.Length)
                {
                    return numChoice;
                }
                else
                {
                    Console.WriteLine($"#####\n#Error - Please enter a choice between 1 and {options.Length}.\n#####");
                }
            }
        }

        /// <summary>
        /// Gets a valid integer input from the user within the specified range.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="minValue">The minimum acceptable value.</param>
        /// <param name="maxValue">The maximum acceptable value.</param>
        /// <returns>A valid integer within the specified range.</returns>
        public static int GetValidIntegerInput(string prompt, int minValue, int maxValue)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result) && result >= minValue && result <= maxValue)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine($"#####\n#Error - Supplied value must be an integer between {minValue} and {maxValue}, please try again.\n#####");
                }
            }
        }
    }

