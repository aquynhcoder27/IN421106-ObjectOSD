namespace EduHub.Ch01.Domain;

/// <summary>
/// Represents a member's registration for an event.
/// </summary>
public sealed class Registration
{
    private Registration(Guid id, Guid memberId, Guid eventId, DateTime registeredAt, string status)
    {
        Id = id;
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = registeredAt;
        Status = status;
    }

    /// <summary>Gets the unique identifier of the registration.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the identifier of the registered member.</summary>
    public Guid MemberId { get; private set; }

    /// <summary>Gets the identifier of the event.</summary>
    public Guid EventId { get; private set; }

    /// <summary>Gets the UTC time when the registration was created.</summary>
    public DateTime RegisteredAt { get; private set; }

    /// <summary>Gets the current registration status.</summary>
    public string Status { get; private set; }

    /// <summary>Gets the optional check-in associated with this registration.</summary>
    public CheckIn? CheckIn { get; private set; }

    /// <summary>
    /// Creates a pending registration for a member and event.
    /// </summary>
    /// <param name="memberId">The identifier of the member.</param>
    /// <param name="eventId">The identifier of the event.</param>
    /// <returns>A pending registration with a generated identifier.</returns>
    /// <exception cref="ArgumentException">An identifier is empty.</exception>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
        {
            throw new ArgumentException("Member identifier cannot be empty.", nameof(memberId));
        }

        if (eventId == Guid.Empty)
        {
            throw new ArgumentException("Event identifier cannot be empty.", nameof(eventId));
        }

        return new Registration(Guid.NewGuid(), memberId, eventId, DateTime.UtcNow, RegistrationStatus.Pending);
    }

    /// <summary>
    /// Confirms a pending registration.
    /// </summary>
    /// <exception cref="InvalidOperationException">The registration is not pending.</exception>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
        {
            throw new InvalidOperationException("Only pending registrations can be confirmed.");
        }

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>
    /// Cancels a pending or confirmed registration.
    /// </summary>
    /// <exception cref="InvalidOperationException">The registration is already cancelled.</exception>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
        {
            throw new InvalidOperationException("A cancelled registration cannot be cancelled again.");
        }

        Status = RegistrationStatus.Cancelled;
    }

    internal void AttachCheckIn(CheckIn checkIn)
    {
        ArgumentNullException.ThrowIfNull(checkIn);

        if (CheckIn is not null)
        {
            throw new InvalidOperationException("This registration already has a check-in.");
        }

        CheckIn = checkIn;
    }
}
