CREATE TABLE [Event]
(
	[EventID] INT IDENTITY (1, 1) PRIMARY KEY,
	[EventName] VARCHAR(100) NOT NULL,
	[PoliticalPartyID] INT NOT NULL,
	[PoliticianID] INT NOT NULL,
	[EventLocation] VARCHAR(100) NOT NULL,
	[EventBeginDate] DATETIME NOT NULL DEFAULT(GETDATE()),
	[EventEndDate] DATETIME NOT NULL DEFAULT(GETDATE()),
	[Agenda] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(500) NOT NULL,
	[AddedOn] DATETIME,
	[UpdatedOn] DATETIME
	--EventLogo
	--EventLogoFileName
	--AddedBy
	--UpdatedBy
)