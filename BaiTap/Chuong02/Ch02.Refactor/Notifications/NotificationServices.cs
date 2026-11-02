namespace EduHub.Ch02.Notifications;

public sealed class EmailNotificationService : INotificationService
{
    public void Send(string to, string message) => Console.WriteLine($"[EMAIL] to {to}: {message}");
}

public sealed class SmsNotificationService : INotificationService
{
    public void Send(string to, string message) => Console.WriteLine($"[SMS] to {to}: {message}");
}

public sealed class SlackNotificationService : INotificationService
{
    public void Send(string to, string message) => Console.WriteLine($"[SLACK] to {to}: {message}");
}
