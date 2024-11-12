namespace CAB201HOSPAPP;
/// <summary>
/// Represents a surgeon in the hospital system.
/// Inherits from the Staff class.
/// </summary>
public class Surgeon : Staff
{
    /// <summary>
    /// Gets or sets the specialty of the surgeon.
    /// </summary>
    public string? Speciality { get; set; }
}