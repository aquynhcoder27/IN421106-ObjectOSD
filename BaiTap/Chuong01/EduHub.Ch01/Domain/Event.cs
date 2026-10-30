namespace EduHub.Ch01.Domain;

/// <summary>
/// Represents an event organized by a club.
/// </summary>
public sealed class Event
{
    private Event(Guid id, Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        Id = id;
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
        IsOnline = isOnline;
    }

    /// <summary>Gets the unique identifier of the event.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the identifier of the club organizing the event.</summary>
    public Guid ClubId { get; private set; }

    /// <summary>Gets the event title.</summary>
    public string Title { get; private set; }

    /// <summary>Gets the scheduled date and time of the event.</summary>
    public DateTime Date { get; private set; }

    /// <summary>Gets the maximum number of registrations.</summary>
    public int Capacity { get; private set; }

    /// <summary>Gets whether the event is online.</summary>
    public bool IsOnline { get; private set; }

    /// <summary>
    /// Creates an event after validating its club, title, and capacity.
    /// </summary>
    /// <param name="clubId">The identifier of the organizing club.</param>
    /// <param name="title">The event title.</param>
    /// <param name="date">The scheduled date and time.</param>
    /// <param name="capacity">The maximum number of registrations.</param>
    /// <param name="isOnline">Whether the event is online.</param>
    /// <returns>A validated event with a generated identifier.</returns>
    /// <exception cref="ArgumentException">The club identifier is empty or the title is blank.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is not positive.</exception>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline = false)
    {
        if (clubId == Guid.Empty)
        {
            throw new ArgumentException("Club identifier cannot be empty.", nameof(clubId));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Event title cannot be empty.", nameof(title));
        }

        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");
        }

        return new Event(Guid.NewGuid(), clubId, title.Trim(), date, capacity, isOnline);
    }

    /// <summary>
    /// Determines whether another registration fits within the event capacity.
    /// </summary>
    /// <param name="registeredCount">The current number of registrations.</param>
    /// <returns><see langword="true"/> when capacity remains; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="registeredCount"/> is negative.</exception>
    public bool HasCapacity(int registeredCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(registeredCount);
        return registeredCount < Capacity;
    }
}
