namespace CAB201HOSPAPP;

/// <summary>
/// Interface for handling data operations such as retrieving, registering, and updating users.
/// </summary>
        public interface IDataHandler
        {
            List<User> GetRegisteredUsers();
            List<Patient> GetRegisteredPatients();
            void RegisterUser(User user);
            bool CheckIfStaffIdExists(int staffId);
            bool CheckIfEmailExists(string email);
            void UpdateUser(User user);
        }

        /// <summary>
        /// Implementation of IDataHandler for handling user data operations.
        /// </summary>
        public class DataHandler : IDataHandler
        {
            private readonly IDataBase _database;

            public DataHandler(IDataBase database)
            {
                _database = database;
            }

            public List<User> GetRegisteredUsers()
            {
                return _database.GetUsers();
            }

            public List<Patient> GetRegisteredPatients()
            {
                return _database.GetUsers().OfType<Patient>().ToList();
            }

            public void RegisterUser(User user)
            {
                // Check if email is already registered to avoid duplication
                if (!_database.IsEmailRegistered(user.Email))
                {
                    _database.AddUser(user);
                }
                else
                {
                    throw new InvalidOperationException("Email is already registered.");
                }
            }

            public bool CheckIfStaffIdExists(int staffId)
            {
                return _database.IsStaffIdRegistered(staffId);
            }

            public bool CheckIfEmailExists(string email)
            {
                return _database.IsEmailRegistered(email);
            }

            public void UpdateUser(User user)
            {
                var users = _database.GetUsers();
                var existingUser = users.FirstOrDefault(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase));
                if (existingUser != null)
                {
                    // Update common User properties
                    existingUser.Password = user.Password;

                    // Update properties specific to Patient
                    if (user is Patient patient && existingUser is Patient existingPatient)
                    {
                        existingPatient.isCheckedIn = patient.isCheckedIn;
                        existingPatient.HasHadSurgery = patient.HasHadSurgery;
                        existingPatient.RoomNumber = patient.RoomNumber;
                        existingPatient.FloorNumber = patient.FloorNumber;
                        existingPatient.AssignedSurgeon = patient.AssignedSurgeon;
                        existingPatient.surgeryDateTime = patient.surgeryDateTime;
                        existingPatient.SurgeryAssignmentSequence = patient.SurgeryAssignmentSequence;
                    }

                    // Update properties specific to FloorManager
                    if (user is FloorManager floorManager && existingUser is FloorManager existingFloorManager)
                    {
                        existingFloorManager.FloorNumber = floorManager.FloorNumber;
                    }

                    // Update properties specific to Surgeon
                    if (user is Surgeon surgeon && existingUser is Surgeon existingSurgeon)
                    {
                        existingSurgeon.Speciality = surgeon.Speciality;
                    }
                }
                else
                {
                    Console.WriteLine("User not found to update.");
                }
            }
        }
    

