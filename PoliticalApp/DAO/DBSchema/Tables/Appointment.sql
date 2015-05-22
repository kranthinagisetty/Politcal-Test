CREATE TABLE [Appointment]
(
	[AppointmentID] INT IDENTITY(1, 1) PRIMARY KEY,
	[CitizenID] INT NOT NULL,
	[AppointmentDate] DATETIME NOT NULL DEFAULT(GETDATE()),
	[Message] VARCHAR(500) NOT NULL,
	[AppointmentStatusID] TINYINT NOT NULL,
	[AddedOn] DATETIME,
	[UpdatedOn] DATETIME
	--Name
	--MobileNo
	--Email
	--AddedBy
	--UpdatedBy
)

ALTER TABLE [Appointment] WITH CHECK ADD CONSTRAINT [FK_Appintment_CitizenID] FOREIGN KEY ([CitizenID])
REFERENCES  [Citizen]([CitizenID])
GO