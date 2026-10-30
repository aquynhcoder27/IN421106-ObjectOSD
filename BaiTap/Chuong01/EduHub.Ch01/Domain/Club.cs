namespace EduHub.Ch01.Domain;

/// <summary>
/// Represents a student club and its members.
/// </summary>
public sealed class Club
{
    private readonly List<Member> _members = [];

    private Club(Guid id, string name, string description, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    /// <summary>Gets the unique identifier of the club.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the club's name.</summary>
    public string Name { get; private set; }

    /// <summary>Gets the club's description.</summary>
    public string Description { get; private set; }

    /// <summary>Gets the UTC time when the club was created.</summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>Gets a read-only view of the club's members.</summary>
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    /// <summary>
    /// Creates a club after validating its name and description.
    /// </summary>
    /// <param name="name">The club's name.</param>
    /// <param name="description">A description of the club.</param>
    /// <returns>A validated club with a generated identifier.</returns>
    /// <exception cref="ArgumentException">The name or description is empty.</exception>
    public static Club Create(string name, string description)
    {
        var normalizedName = RequiredText(name, nameof(name));
        var normalizedDescription = RequiredText(description, nameof(description));

        return new Club(Guid.NewGuid(), normalizedName, normalizedDescription, DateTime.UtcNow);
    }

    /// <summary>
    /// Adds a member unless that member is already in the club.
    /// </summary>
    /// <param name="member">The member to add.</param>
    /// <exception cref="ArgumentNullException"><paramref name="member"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">The member is already in the club.</exception>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);

        if (_members.Any(existingMember => existingMember.Id == member.Id))
        {
            throw new InvalidOperationException("The member is already in this club.");
        }

        _members.Add(member);
    }

    /// <summary>
    /// Returns the number of members in the club.
    /// </summary>
    /// <returns>The current member count.</returns>
    public int MemberCount() => _members.Count;

    private static string RequiredText(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be empty.", parameterName);
        }

        return value.Trim();
    }
}
