using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Repositories;

namespace EduHub.Ch02.Services;

public sealed class ClubService(
    IClubRepository clubs,
    IMemberRepository members,
    INotificationService notifier,
    IAppLogger logger) : IClubService
{
    public Club CreateClub(string name, string description, string presidentEmail)
    {
        var club = Club.Create(name, description);
        clubs.Add(club);
        logger.Info($"Club created: {club.Name}");
        notifier.Send(presidentEmail, $"Club '{club.Name}' has been created");
        return club;
    }

    public Member AddMember(Guid clubId, string memberName, string email)
    {
        var club = clubs.GetById(clubId) ?? throw new InvalidOperationException("Club not found");
        if (!club.HasRoom(members.GetByClub(clubId).Count))
            throw new InvalidOperationException($"Club is full (max {Club.MaxMembers} members)");

        var member = new Member { Id = Guid.NewGuid(), Name = memberName, Email = email, ClubId = clubId };
        members.Add(member);
        logger.Info($"Member {member.Name} joined {club.Name}");
        notifier.Send(email, $"Welcome to {club.Name}!");
        return member;
    }
}
