# SOLID analysis of the starter `ClubManager`

The line numbers below refer to `BaiGiang/ch02/IN4211-ch02-solid-patterns-git/starter/Ch02.Refactor/ClubManager.cs`.

| Location | Principle | Violation and consequence |
|---|---|---|
| Lines 28–32; methods 35–116 | SRP | `ClubManager` owns club/member/event data, validation, logging, notifications and reporting. Each concern gives the class another reason to change and makes focused tests difficult. |
| Lines 47–63 and 78–83 | DIP | Business operations call `File.AppendAllText` and `Console.WriteLine` directly. They cannot substitute logging or notification implementations without changing the manager. |
| Lines 50–63 and 81–83 | OCP | Notification behavior is selected with email-domain conditionals and fixed email/Slack calls. Adding a channel requires editing existing business methods. |
| Lines 86–99 | SRP / OCP | `CreateEvent` both creates an event and decides who receives notifications. New event-created reactions require editing the method, and the service is coupled to delivery details. |
| Lines 101–116 | SRP / OCP | `PrintClubReport` calculates data, formats console output and writes logs. Supporting CSV or another output format requires changing this method. |
| Lines 30–31, 69–76, 90–97 | ISP / DIP | The manager holds concrete lists and performs storage queries itself. Services cannot depend on small repository contracts or substitute storage implementations. |

The refactor moves business rules into Domain, coordinates use cases in services, and uses small interfaces at storage, logging and notification boundaries. Factory, Decorator, Strategy and Observer isolate the extension points requested by the assignment.
