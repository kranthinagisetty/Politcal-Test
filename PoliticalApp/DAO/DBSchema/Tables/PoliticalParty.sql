CREATE TABLE [PoliticalParty]
(
	[PoliticalPartyID] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[PartyName] VARCHAR (100) NOT NULL,
	[President] VARCHAR (100) NOT NULL,
	[FoundedOn] DATETIME NOT NULL DEFAULT(GETDATE()),
	[Address] VARCHAR(500) NOT NULL,
	[History] VARCHAR(500) NOT NULL,
	[AddedOn] DATETIME,
	[UpdatedOn] DATETIME,
	--PartyLogo
	--LogoFileName
)
