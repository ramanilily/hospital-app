namespace CAB201HOSPAPP;
/// <summary>
/// Provides base functionality for registration handlers, specifically for collecting basic user details.
/// </summary>
public class BaseRegistrationHandler
{
    /// <summary>
    /// Collects basic user details such as name, age, mobile number, email, and password.
    /// </summary>
    /// <param name="user">The user object to populate with details.</param>
    /// <param name="registeredUsers">List of registered users for email uniqueness check.</param>
    /// <param name="minAge">Minimum allowable age for the user.</param>
    /// <param name="maxAge">Maximum allowable age for the user.</param>
    protected void CollectBasicDetails(User user, List<User> registeredUsers, int minAge, int maxAge)
    {
        user.Name = UserInputHandler.RequestValidName();
        user.Age = UserInputHandler.RequestValidAge("Please enter in your age:", minAge, maxAge);
        user.MobileNumber = UserInputHandler.RequestValidMobileNumber();
        user.Email = UserInputHandler.RequestValidEmail(registeredUsers);
        user.Password = UserInputHandler.RequestValidPassword();
    }
}