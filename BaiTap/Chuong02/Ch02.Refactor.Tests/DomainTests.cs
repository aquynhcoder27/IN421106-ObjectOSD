using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

public class DomainTests
{
    [Fact]
    public void ClubCreate_WithValidName_ReturnsClubWithGeneratedId()
    {
        // Arrange
        const string name = "Tech Club";

        // Act
        var club = Club.Create(name, "Technology community");

        // Assert
        Assert.Equal(name, club.Name);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ClubCreate_WithBlankName_ThrowsArgumentException(string name)
    {
        // Arrange
        const string description = "Description";

        // Act
        var act = () => Club.Create(name, description);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void ClubCreate_WithNameLongerThanLimit_ThrowsArgumentException()
    {
        // Arrange
        var name = new string('x', Club.MaxNameLength + 1);

        // Act
        var act = () => Club.Create(name, "Description");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData(49, true)]
    [InlineData(50, false)]
    public void ClubHasRoom_AtCapacityBoundary_ReturnsExpectedResult(int count, bool expected)
    {
        // Arrange
        var club = Club.Create("Tech Club", "Description");

        // Act
        var hasRoom = club.HasRoom(count);

        // Assert
        Assert.Equal(expected, hasRoom);
    }

    [Fact]
    public void EventRegister_NewMember_ReturnsTrueAndStoresMember()
    {
        // Arrange
        var ev = NewEvent();
        var memberId = Guid.NewGuid();

        // Act
        var registered = ev.Register(memberId);

        // Assert
        Assert.True(registered);
        Assert.Contains(memberId, ev.RegisteredMemberIds);
    }

    [Fact]
    public void EventRegister_DuplicateMember_ReturnsFalseAndKeepsSingleEntry()
    {
        // Arrange
        var ev = NewEvent();
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        var registeredAgain = ev.Register(memberId);

        // Assert
        Assert.False(registeredAgain);
        Assert.Single(ev.RegisteredMemberIds);
    }

    [Fact]
    public void EventCheckIn_RegisteredMember_RecordsCheckIn()
    {
        // Arrange
        var ev = NewEvent();
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        ev.CheckIn(memberId);

        // Assert
        Assert.Contains(memberId, ev.CheckedInMemberIds);
    }

    [Fact]
    public void EventCheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var ev = NewEvent();
        var memberId = Guid.NewGuid();

        // Act
        var act = () => ev.CheckIn(memberId);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void EventAttendanceRate_WithNoRegistrations_IsZero()
    {
        // Arrange
        var ev = NewEvent();

        // Act
        var rate = ev.AttendanceRate;

        // Assert
        Assert.Equal(0, rate);
    }

    [Fact]
    public void EventAttendanceRate_OneOfTwoAttendeesCheckedIn_IsHalf()
    {
        // Arrange
        var ev = NewEvent();
        var checkedInMember = Guid.NewGuid();
        var registeredMember = Guid.NewGuid();
        ev.Register(checkedInMember);
        ev.Register(registeredMember);
        ev.CheckIn(checkedInMember);

        // Act
        var rate = ev.AttendanceRate;

        // Assert
        Assert.Equal(0.5, rate);
    }

    private static Event NewEvent() => new()
    {
        Id = Guid.NewGuid(),
        ClubId = Guid.NewGuid(),
        Title = "SOLID workshop",
        Date = new DateTime(2026, 11, 20)
    };
}
