using System.Text;

namespace EduHub.Ch02.Patterns.Strategies;

public sealed record ClubStat(string ClubName, int Members, int Events, int Registered, int CheckedIn);

public interface IReportStrategy
{
    string Render(IReadOnlyList<ClubStat> stats);
}

public sealed class ConsoleReportStrategy : IReportStrategy
{
    public string Render(IReadOnlyList<ClubStat> stats)
    {
        var output = new StringBuilder();
        foreach (var stat in stats)
            output.AppendLine($"Club: {stat.ClubName} | Members: {stat.Members} | Events: {stat.Events} | Registered: {stat.Registered} | Checked-in: {stat.CheckedIn}");
        return output.ToString();
    }
}

public sealed class CsvReportStrategy : IReportStrategy
{
    public string Render(IReadOnlyList<ClubStat> stats)
    {
        var output = new StringBuilder("Club,Members,Events,Registered,CheckedIn\n");
        foreach (var stat in stats)
        {
            var escapedName = stat.ClubName.Replace("\"", "\"\"");
            output.AppendLine($"\"{escapedName}\",{stat.Members},{stat.Events},{stat.Registered},{stat.CheckedIn}");
        }
        return output.ToString();
    }
}
