CREATE TABLE [Volunteer]
(
	[VolunteerID] INT IDENTITY(1, 1) PRIMARY KEY,
	[CitizenID] INT NOT NULL,
	[EventID] INT NOT NULL,
	[AddedOn] DATETIME,
	[UpdatedOn] DATETIME
)