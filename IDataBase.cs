namespace CAB201HOSPAPP;

    /// <summary>
    /// Defines methods for accessing and modifying user data in the database.
    /// </summary>
    public interface IDataBase
    { 
        List<User> GetUsers();
        void AddUser(User user);
        bool IsStaffIdRegistered(int staffId);
        bool IsEmailRegistered(string email);
    }

    /// <summary>
    /// Implementation of the IDataBase interface for managing user data.
    /// </summary>
       public class Database : IDataBase
        {
            private List<User> users = new List<User>();
            private List<Patient> patients = new List<Patient>();

            public List<User> GetUsers() => users;
        
            public List<Patient> GetPatients() => patients;

            public void AddUser(User user)
            {
                users.Add(user);
            }
            
            public bool IsStaffIdRegistered(int staffId)
            {
                // Check if a staff ID exists among registered users of type Staff or derived classes
                return users.OfType<Staff>().Any(u => u.Staffid == staffId);
            }

            public bool IsEmailRegistered(string email)
            {
                // Ensure the email check is case-insensitive
                return users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
        }
    
