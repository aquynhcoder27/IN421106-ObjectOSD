namespace EduHub.Ch02.Logging;

public sealed class FileLogger(string path) : IAppLogger
{
    public void Info(string message) =>
        File.AppendAllText(path, $"[{DateTime.Now:s}] {message}{Environment.NewLine}");
}
