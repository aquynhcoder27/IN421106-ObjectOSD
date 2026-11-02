using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Patterns.Observers;
using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;
using EduHub.Ch02.Services;

IAppLogger logger = new ConsoleLogger();
INotificationServiceFactory notificationFactory = new NotificationServiceFactory();
INotificationService notifier = notificationFactory.Create(NotificationChannel.Email);

IClubRepository clubs = new LoggingClubRepository(new InMemoryClubRepository(), logger);
IMemberRepository members = new InMemoryMemberRepository();
IEventRepository events = new InMemoryEventRepository();

IClubService clubService = new ClubService(clubs, members, notifier, logger);
IEventService eventService = new EventService(clubs, members, events);
IReportService reportService = new ReportService(clubs, members, events);

Console.WriteLine("== Create a club and add members ==");
var club = clubService.CreateClub("Tech Club", "Technology community", "president@example.edu");
var member = clubService.AddMember(club.Id, "Nguyen Van A", "a@student.example.edu");
clubService.AddMember(club.Id, "Tran Thi B", "b@student.example.edu");

Console.WriteLine("\n== Observer: notify listeners about a new event ==");
eventService.Subscribe(new LogNotifier(logger));
eventService.Subscribe(new EmailNotifier(members, notifier));
var ev = eventService.CreateEvent(club.Id, "SOLID workshop", new DateTime(2026, 11, 20));
eventService.Register(ev.Id, member.Id);
eventService.CheckIn(ev.Id, member.Id);

Console.WriteLine("\n== Strategy: console report ==");
Console.Write(reportService.Generate(new ConsoleReportStrategy()));
Console.WriteLine("== Strategy: CSV report ==");
Console.Write(reportService.Generate(new CsvReportStrategy()));

Console.WriteLine("== Factory: notification channels ==");
foreach (var channel in Enum.GetValues<NotificationChannel>())
    notificationFactory.Create(channel).Send(member.Email, $"Test message ({channel})");

Console.WriteLine("== Domain validation ==");
try
{
    clubService.CreateClub("", "Invalid club", "president@example.edu");
}
catch (ArgumentException exception)
{
    Console.WriteLine($"Expected validation error: {exception.Message}");
}
