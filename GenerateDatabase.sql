USE [Drinks]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 25/02/2014 12:38:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Username] [nvarchar](128) NOT NULL,
	[Password] [binary](64) NOT NULL,
	[Salt] [binary](64) NOT NULL,
	[Permissions] [tinyint] NOT NULL,
	[BadgeId] [varchar](16) NULL,
UNIQUE NONCLUSTERED 
(
	[BadgeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Permissions]
GO

CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [money] NULL,
	[UserId] [int] NOT NULL,
	[ExecutorUserId] [int] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Transactions] ADD  DEFAULT (getdate()) FOR [Timestamp]
GO

CREATE TABLE [dbo].[Products](
	[Id] [tinyint] IDENTITY(0,1) NOT NULL,
	[Name] [varchar](11) NOT NULL,
	[Price] [money] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO