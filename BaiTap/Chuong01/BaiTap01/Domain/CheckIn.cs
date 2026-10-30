namespace EduHub.Ch01.Domain;

/// <summary>
/// Records that a confirmed registration attended an event.
/// </summary>
public sealed class CheckIn
{
    private CheckIn(Guid id, Guid registrationId, DateTime checkedInAt, string method)
    {
        Id = id;
        RegistrationId = registrationId;
        CheckedInAt = checkedInAt;
        Method = method;
    }

    /// <summary>Gets the unique identifier of the check-in record.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the identifier of the related registration.</summary>
    public Guid RegistrationId { get; private set; }

    /// <summary>Gets the UTC time when the check-in was recorded.</summary>
    public DateTime CheckedInAt { get; private set; }

    /// <summary>Gets the method used to record attendance.</summary>
    public string Method { get; private set; }

    /// <summary>
    /// Creates a check-in for a confirmed registration using a supported method.
    /// </summary>
    /// <param name="registration">The confirmed registration being checked in.</param>
    /// <param name="method">The attendance method from <see cref="CheckInMethod"/>.</param>
    /// <returns>A check-in record with a generated identifier.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="registration"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="method"/> is unsupported.</exception>
    /// <exception cref="InvalidOperationException">The registration is not confirmed.</exception>
    public static CheckIn Create(Registration registration, string method)
    {
        ArgumentNullException.ThrowIfNull(registration);

        if (registration.Status != RegistrationStatus.Confirmed)
        {
            throw new InvalidOperationException("Only confirmed registrations can check in.");
        }

        if (method is not CheckInMethod.Manual and not CheckInMethod.QrCode)
        {
            throw new ArgumentException("The check-in method is unsupported.", nameof(method));
        }

        return new CheckIn(Guid.NewGuid(), registration.Id, DateTime.UtcNow, method);
    }

    /// <summary>
    /// Returns a readable description of this check-in.
    /// </summary>
    /// <returns>A summary containing the registration, time, and method.</returns>
    public string Summary() => $"Registration {RegistrationId} checked in at {CheckedInAt:O} using {Method}.";
}
