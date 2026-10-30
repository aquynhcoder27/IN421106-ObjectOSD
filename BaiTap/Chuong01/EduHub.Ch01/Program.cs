using EduHub.Ch01.Domain;
using ClubEvent = EduHub.Ch01.Domain.Event;

internal static class Program
{
    private const string Separator = "============================================================";
    private const string ClubName = "Tech Club";
    private const string ClubDescription = "A student community for technology, software, and innovation.";
    private const string EventTitle = "Workshop AI";
    private const int EventCapacity = 50;
    private const int NumberOfRegistrations = 2;
    private const int EventDateOffsetDays = 7;

    private static readonly (string Name, string Email, string StudentCode)[] SampleMembers =
    [
        ("An Nguyen", "an.nguyen@example.edu", "SV001"),
        ("Binh Tran", "binh.tran@example.edu", "SV002"),
        ("Chi Le", "chi.le@example.edu", "SV003")
    ];

    private static void Main()
    {
        PrintSection("1. Create club");
        var club = Club.Create(ClubName, ClubDescription);
        Console.WriteLine($"{club.Name}: {club.Description}");

        PrintSection("2. Add members");
        var members = SampleMembers
            .Select(sample => Member.Create(sample.Name, sample.Email, sample.StudentCode))
            .ToList();

        foreach (var member in members)
        {
            club.AddMember(member);
            Console.WriteLine($"Added {member}");
        }

        Console.WriteLine($"Club member count: {club.MemberCount()}");

        PrintSection("3. Create event");
        var eventDate = DateTime.UtcNow.AddDays(EventDateOffsetDays);
        var eventItem = ClubEvent.Create(club.Id, EventTitle, eventDate, EventCapacity);
        Console.WriteLine($"{eventItem.Title} | {eventItem.Date:u} | capacity {eventItem.Capacity}");

        PrintSection("4. Register and confirm attendees");
        var registrations = new List<Registration>();
        foreach (var member in members.Take(NumberOfRegistrations))
        {
            if (!eventItem.HasCapacity(registrations.Count))
            {
                throw new InvalidOperationException("The event has no remaining capacity.");
            }

            var registration = Registration.Create(member.Id, eventItem.Id);
            Console.WriteLine($"{member.Name}: {registration.Status}");
            registration.Confirm();
            Console.WriteLine($"{member.Name}: {registration.Status}");
            registrations.Add(registration);
        }

        PrintSection("5. Record check-in");
        var checkIns = new List<CheckIn>
        {
            CheckIn.Create(registrations.First(), CheckInMethod.Manual)
        };
        Console.WriteLine(checkIns.First().Summary());

        PrintSection("6. Attendance report");
        var namesByMemberId = members.ToDictionary(member => member.Id, member => member.Name);
        var checkedInRegistrationIds = checkIns
            .Select(checkIn => checkIn.RegistrationId)
            .ToHashSet();

        Console.WriteLine($"Registrations: {registrations.Count}");
        Console.WriteLine($"Check-ins: {checkIns.Count}");
        Console.WriteLine("Attendees:");
        foreach (var registration in registrations)
        {
            var attendanceState = checkedInRegistrationIds.Contains(registration.Id)
                ? "Checked in"
                : "Not checked in";
            Console.WriteLine($"- {namesByMemberId[registration.MemberId]} ({attendanceState})");
        }

        PrintSection("7. Demonstrate an invalid state transition");
        var cancelledRegistration = Registration.Create(members.Last().Id, eventItem.Id);
        cancelledRegistration.Cancel();
        try
        {
            cancelledRegistration.Confirm();
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"Expected business rule: {exception.Message}");
        }

        PrintSection("Demo complete");
    }

    private static void PrintSection(string title)
    {
        Console.WriteLine();
        Console.WriteLine(Separator);
        Console.WriteLine(title);
        Console.WriteLine(Separator);
    }
}
