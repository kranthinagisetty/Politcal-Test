CREATE TABLE [FeedBack]
(
	[FeedBackID] INT IDENTITY(1, 1) PRIMARY KEY,
	[Subject] VARCHAR (100) NOT NULL,
	[Description] VARCHAR (500) NOT NULL,
	[AddedOn] DATETIME,
	[UpdatedOn] DATETIME
	--[Name]
	--[MobileNo]
	--[Email]
	--[UpdatedBy]
)
