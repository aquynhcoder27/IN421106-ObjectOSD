namespace EduHub.Ch01.Domain;

/// <summary>
/// Defines the lifecycle states available to an event registration.
/// </summary>
public static class RegistrationStatus
{
    /// <summary>Indicates that a registration is awaiting confirmation.</summary>
    public const string Pending = "Pending";

    /// <summary>Indicates that a registration has been confirmed.</summary>
    public const string Confirmed = "Confirmed";

    /// <summary>Indicates that a registration has been cancelled.</summary>
    public const string Cancelled = "Cancelled";
}
