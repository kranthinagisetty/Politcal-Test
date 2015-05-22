CREATE TABLE [Complaint]
(
	[ComplaintID] INT IDENTITY(1,1) PRIMARY KEY,
	[Subject] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(500) NOT NULL,
	[Status] TINYINT NOT NULL,
	[AddedOn] DATETIME,
	[UpdatedOn] DATETIME
	--[AddedBy]
	--[UpdatedBy]
	--[Name]
	--[MobileNo]
	--[Email]
)