using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;

namespace EduHub.Ch02.Services;

public sealed class ReportService(IClubRepository clubs, IMemberRepository members, IEventRepository events) : IReportService
{
    public string Generate(IReportStrategy strategy)
    {
        var stats = clubs.GetAll().Select(club =>
        {
            var clubEvents = events.GetByClub(club.Id);
            return new ClubStat(club.Name, members.GetByClub(club.Id).Count, clubEvents.Count,
                clubEvents.Sum(ev => ev.RegisteredMemberIds.Count), clubEvents.Sum(ev => ev.CheckedInMemberIds.Count));
        }).ToList();

        return strategy.Render(stats);
    }
}
