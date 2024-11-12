using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace CAB201HOSPAPP;

/// <summary>
/// Represents a patient in the hospital system who inherits from the User class.
/// </summary>
public class Patient : User
{
    /// <summary>
    /// gets or sets the room number assigned to the patient.
    /// </summary>
    public int RoomNumber { get; set; }
    /// <summary>
    /// gets or sets the floor number where the patient's room is located.
    /// </summary>
    public int FloorNumber { get; set; }
    /// <summary>
    /// gets or sets a value indicating whether the patient is checked into the hospital.
    /// </summary>
    public bool isCheckedIn { get; set; }
    /// <summary>
    /// gets or sets the surgeon assigned to the patient, if any.
    /// </summary>
    public Surgeon? AssignedSurgeon { get; set; }
    /// <summary>
    /// gets or sets the date and time of the patient's surgery, if scheduled.
    /// </summary>
    public DateTime? surgeryDateTime { get; set; }
    /// <summary>
    /// gets or sets a value indicating whether the patient has had surgery.
    /// </summary>
    public bool HasHadSurgery { get; set; } = false;
    /// <summary>
    /// gets or sets the surgery assignment sequence number for this patient.
    /// </summary>
    public int? SurgeryAssignmentSequence { get; set; }
}