using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Repositories;

namespace EduHub.Ch02.Patterns.Observers;

public sealed class LogNotifier(IAppLogger logger) : IEventObserver
{
    public void OnEventCreated(Event ev, Club club) => logger.Info($"Event created: '{ev.Title}' ({club.Name})");
}

public sealed class EmailNotifier(IMemberRepository members, INotificationService notifier) : IEventObserver
{
    public void OnEventCreated(Event ev, Club club)
    {
        foreach (var member in members.GetByClub(club.Id))
            notifier.Send(member.Email, $"New event '{ev.Title}' at {club.Name} on {ev.Date:dd/MM/yyyy}");
    }
}
