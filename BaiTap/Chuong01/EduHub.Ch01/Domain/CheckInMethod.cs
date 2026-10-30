namespace EduHub.Ch01.Domain;

/// <summary>
/// Defines the supported ways to record event attendance.
/// </summary>
public static class CheckInMethod
{
    /// <summary>Records attendance manually.</summary>
    public const string Manual = "Manual";

    /// <summary>Records attendance with a QR code.</summary>
    public const string QrCode = "QrCode";
}
