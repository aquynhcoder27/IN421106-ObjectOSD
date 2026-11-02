using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;

namespace EduHub.Ch02.Repositories;

/// <summary>Decorator adds repository logging without changing the wrapped repository.</summary>
public sealed class LoggingClubRepository(IClubRepository inner, IAppLogger logger) : IClubRepository
{
    public IReadOnlyList<Club> GetAll()
    {
        logger.Info("[repo] GetAll clubs");
        return inner.GetAll();
    }

    public Club? GetById(Guid id)
    {
        logger.Info($"[repo] GetById {id}");
        return inner.GetById(id);
    }

    public void Add(Club club)
    {
        logger.Info($"[repo] Add club '{club.Name}'");
        inner.Add(club);
    }
}
