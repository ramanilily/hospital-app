using System.Text.RegularExpressions;

namespace CAB201HOSPAPP
{
    /// <summary>
    /// Represents a staff member who inherits from the User class.
    /// </summary>
    public class Staff : User
    {

        public int Staffid { get; set; }

        // Static list to keep track of registered staff IDs
        private static List<int> registeredStaffIds = new List<int>();
        
        /// <summary>
        /// Validates the staff ID to ensure it is a 3-digit number and unique among registered staff members.
        /// </summary>
        protected void ValidateStaffId()
        {
            bool validStaffId = false;
            while (!validStaffId)
            {
                Console.WriteLine("Please enter in your staff ID:");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int staffId) && staffId >= 100 && staffId <= 999)
                {
                    if (registeredStaffIds.Contains(staffId))
                    {
                        Console.WriteLine(
                            "#####\n#Error - Staff ID is already registered, please try again.\n#####");
                    }
                    else
                    {
                        Staffid = staffId;
                        registeredStaffIds.Add(staffId); // Add the ID to the registered list
                        validStaffId = true;
                    }
                }
                else
                {
                    Console.WriteLine(
                        "#####\n#Error - Supplied staff identification number is invalid, please try again.\n#####");
                }
            }
        }
    }
}

        




        