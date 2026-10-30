# Preliminary Class Diagram — EduHub

The diagram shows the five required domain classes and their main behavior.

~~~mermaid
classDiagram
    class Member {
        +Guid Id
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +Create(name, email, studentCode) Member
        +IsActive() bool
    }

    class Club {
        +Guid Id
        +string Name
        +string Description
        +DateTime CreatedAt
        +IReadOnlyList~Member~ Members
        +Create(name, description) Club
        +AddMember(member) void
        +MemberCount() int
    }

    class Event {
        +Guid Id
        +Guid ClubId
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +Create(clubId, title, date, capacity, isOnline) Event
        +HasCapacity(registeredCount) bool
    }

    class Registration {
        +Guid Id
        +Guid MemberId
        +Guid EventId
        +DateTime RegisteredAt
        +string Status
        +CheckIn? CheckIn
        +Create(memberId, eventId) Registration
        +Confirm() void
        +Cancel() void
    }

    class CheckIn {
        +Guid Id
        +Guid RegistrationId
        +DateTime CheckedInAt
        +string Method
        +Create(registration, method) CheckIn
        +Summary() string
    }

    Club "1" --> "*" Event : organizes
    Club "*" -- "*" Member : membership
    Member "1" --> "*" Registration : registers
    Event "1" --> "*" Registration : has
    Registration "1" --> "0..1" CheckIn : records
~~~

## Keys and cardinalities

| Class | Primary key | Foreign keys |
|---|---|---|
| Member | Id | — |
| Club | Id | — |
| Event | Id | ClubId → Club.Id |
| Registration | Id | MemberId → Member.Id; EventId → Event.Id |
| CheckIn | Id | RegistrationId → Registration.Id |

- One club organizes many events; each event belongs to one club.
- A member can join many clubs and a club can have many members.
- A member and an event can each have many registrations; each registration links one member to one event.
- A registration has zero or one check-in.
- Registration status values are Pending, Confirmed, and Cancelled; check-in methods are Manual and QrCode.
