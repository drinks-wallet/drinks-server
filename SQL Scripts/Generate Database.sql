USE [Drinks]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 26/02/2014 10:26:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[Products](
	[Id] [tinyint] IDENTITY(0,1) NOT NULL,
	[Name] [varchar](12) NULL,
	[Price] [money] NOT NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [money] NULL,
	[UserId] [int] NOT NULL,
	[ExecutorUserId] [int] NOT NULL,
	[Timestamp] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Username] [nvarchar](128) NOT NULL,
	[Password] [binary](64) NULL,
	[Salt] [binary](64) NULL,
	[IsAdmin] [bit] NOT NULL,
	[BadgeId] [varchar](16) NULL,
	[DiscountPercentage] [int] NOT NULL DEFAULT ((0)),
UNIQUE NONCLUSTERED 
(
	[BadgeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE VIEW [dbo].[UserBalances]
AS SELECT u.id Id, SUM(t.Amount) Balance
FROM Users u JOIN Transactions t
	ON u.Id = t.UserId
GROUP BY u.Id

GO

CREATE View [dbo].[BestCustomers]
AS
SELECT SUM(Amount) as Total, Name
FROM Transactions
JOIN Users
	ON Users.Id = UserId
WHERE Amount < 0
GROUP BY Name

GO