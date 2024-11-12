using System.Text.RegularExpressions;

namespace CAB201HOSPAPP
{
    /// <summary>
    /// Represents an abstract base class for a user, containing common properties and validation methods.
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// gets or sets the name of the user.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// gets or sets the age of the user.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// gets or sets the mobile number of the user.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// gets or sets the email address of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// gets or sets the password of the user.
        /// </summary>
        public string? Password { get; set; }
    }
}
        
      
