# User Stories — EduHub

Stories are grouped by actor. Each story has acceptance criteria in Given/When/Then form.

## Student

### US-01 — Join a club

As a student, I want to apply to join a club, so that I can take part in its activities.

- Given a club is accepting members, when I submit a valid application, then the application is recorded as pending.
- Given I am already an approved member of the club, when I apply again, then the system rejects the duplicate application.

### US-02 — Register for an event

As a student, I want to register for an event, so that I can reserve a place.

- Given an event has available capacity, when I register, then the system creates a pending registration linked to me and the event.
- Given an event is at capacity, when I register, then the system rejects the registration and keeps the existing registrations unchanged.

### US-03 — Check in to an event

As a student, I want to check in to an event, so that my attendance is recorded.

- Given my registration is confirmed, when I check in using a supported method, then the system records one check-in linked to my registration.
- Given my registration is pending or cancelled, when I attempt to check in, then the system rejects the request.

## Club Board

### US-04 — Create an event

As a club board member, I want to create an event for my club, so that members can register.

- Given I manage the club, when I submit an event with a title, date, and positive capacity, then the event is associated with that club.
- Given the capacity is zero or negative, when I submit the event, then the system rejects it and reports the invalid capacity.

### US-05 — View the member list

As a club board member, I want to view my club's member list, so that I can coordinate club activities.

- Given the club has members, when I open its member list, then the system shows each member once.
- Given the club has no members, when I open its member list, then the system shows an empty list.

### US-06 — Export an attendance report

As a club board member, I want to export an event attendance report, so that I can review registrations and attendance.

- Given an event has registrations, when I export its report, then the report includes registration count, check-in count, and attendee names.
- Given an event has no registrations, when I export its report, then the report shows zero registrations and zero check-ins.

## Admin

### US-07 — Manage clubs

As an admin, I want to create and update club records, so that the club directory stays accurate.

- Given a club name and description are valid, when I create the club, then the system assigns an identifier and stores the supplied details.
- Given a club exists, when I update its directory details, then the system saves the changes and retains the same club identifier.

### US-08 — Approve club membership

As an admin, I want to approve membership applications, so that club membership is reviewed consistently.

- Given a membership application is pending, when I approve it, then the application becomes approved and the student is added to the club.
- Given an application is already approved or rejected, when I try to approve it again, then the system rejects the invalid state transition.
