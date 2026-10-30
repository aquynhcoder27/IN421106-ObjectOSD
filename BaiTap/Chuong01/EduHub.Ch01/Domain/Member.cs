namespace EduHub.Ch01.Domain;

/// <summary>
/// Represents a student who can join clubs and register for events.
/// </summary>
public sealed class Member
{
    private Member(Guid id, string name, string email, string studentCode, DateTime joinedAt)
    {
        Id = id;
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = joinedAt;
    }

    /// <summary>Gets the unique identifier of the member.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the member's full name.</summary>
    public string Name { get; private set; }

    /// <summary>Gets the member's email address.</summary>
    public string Email { get; private set; }

    /// <summary>Gets the member's student code.</summary>
    public string StudentCode { get; private set; }

    /// <summary>Gets the UTC time when the member record was created.</summary>
    public DateTime JoinedAt { get; private set; }

    /// <summary>
    /// Creates a member after validating the required identity details.
    /// </summary>
    /// <param name="name">The member's full name.</param>
    /// <param name="email">The member's email address.</param>
    /// <param name="studentCode">The member's student code.</param>
    /// <returns>A validated member with a generated identifier.</returns>
    /// <exception cref="ArgumentException">A required value is empty or the email is malformed.</exception>
    public static Member Create(string name, string email, string studentCode)
    {
        var normalizedName = RequiredText(name, nameof(name));
        var normalizedEmail = RequiredText(email, nameof(email));
        var normalizedStudentCode = RequiredText(studentCode, nameof(studentCode));
        var atIndex = normalizedEmail.IndexOf('@');

        if (atIndex <= 0 || atIndex == normalizedEmail.Length - 1)
        {
            throw new ArgumentException("Email must contain text before and after '@'.", nameof(email));
        }

        return new Member(
            Guid.NewGuid(),
            normalizedName,
            normalizedEmail,
            normalizedStudentCode,
            DateTime.UtcNow);
    }

    /// <summary>
    /// Returns whether the member's join time has started.
    /// </summary>
    /// <returns><see langword="true"/> when the member has joined; otherwise, <see langword="false"/>.</returns>
    public bool IsActive() => JoinedAt <= DateTime.UtcNow;

    /// <summary>
    /// Returns the member's name and student code.
    /// </summary>
    /// <returns>A concise display name for the member.</returns>
    public override string ToString() => $"{Name} ({StudentCode})";

    private static string RequiredText(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be empty.", parameterName);
        }

        return value.Trim();
    }
}
