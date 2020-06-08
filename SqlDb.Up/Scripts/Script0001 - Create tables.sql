CREATE TABLE dbo.Client
	(
	id int NOT NULL IDENTITY (1, 1),
	firstName nvarchar(50) NOT NULL,
	lastName nvarchar(50) NOT NULL,
	email nvarchar(50) NOT NULL,
	gender nvarchar(10) NOT NULL,
	ipAddress nvarchar(20) NOT NULL,
	deleted bit NOT NULL
	) ON [PRIMARY]
GO

ALTER TABLE dbo.Client ADD CONSTRAINT
	PK_Client PRIMARY KEY CLUSTERED 
	(
	id
	) ON [PRIMARY]

GO

ALTER TABLE dbo.Client ADD CONSTRAINT
	DF_Client_createdDate DEFAULT GetUtcDate() FOR createdDate
GO


ALTER TABLE dbo.Client ADD CONSTRAINT
	DF_Client_deleted DEFAULT 0 FOR deleted
GO

CREATE NONCLUSTERED INDEX IX_Client_firstName ON dbo.Client(firstName) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_Client_lastName ON dbo.Client(lastName) ON [PRIMARY]
GO