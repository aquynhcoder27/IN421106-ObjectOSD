using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositories;

public sealed class InMemoryClubRepository : IClubRepository
{
    private readonly List<Club> _clubs = new();
    public IReadOnlyList<Club> GetAll() => _clubs.AsReadOnly();
    public Club? GetById(Guid id) => _clubs.FirstOrDefault(club => club.Id == id);
    public void Add(Club club) => _clubs.Add(club);
}

public sealed class InMemoryMemberRepository : IMemberRepository
{
    private readonly List<Member> _members = new();
    public IReadOnlyList<Member> GetByClub(Guid clubId) => _members.Where(member => member.ClubId == clubId).ToList();
    public Member? GetById(Guid id) => _members.FirstOrDefault(member => member.Id == id);
    public void Add(Member member) => _members.Add(member);
}

public sealed class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _events = new();
    public IReadOnlyList<Event> GetByClub(Guid clubId) => _events.Where(ev => ev.ClubId == clubId).ToList();
    public Event? GetById(Guid id) => _events.FirstOrDefault(ev => ev.Id == id);
    public void Add(Event ev) => _events.Add(ev);
}
