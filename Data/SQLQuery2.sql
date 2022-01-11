USE [PrivacyDB]
GO
/****** Object:  Table [dbo].[PrivacyUser]    Script Date: 2021/12/29 10:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrivacyUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[UserType] [nvarchar](50) NULL,
	[FKId] [bigint] NULL,
 CONSTRAINT [PK_PrivacyUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainee]    Script Date: 2021/12/29 10:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](200) NOT NULL,
	[Interesting] [nvarchar](500) NOT NULL,
	[Location] [nvarchar](500) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Trainee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainer]    Script Date: 2021/12/29 10:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[KindOfTrainer] [nvarchar](500) NOT NULL,
	[Certificate] [nvarchar](50) NULL,
	[DescribeYourself] [nvarchar](max) NULL,
	[Location] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Trainer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PrivacyUser] ON 
GO
INSERT [dbo].[PrivacyUser] ([Id], [Username], [Password], [UserType], [FKId]) VALUES (1, N'1', N'1', N'Trainee', 6)
GO
INSERT [dbo].[PrivacyUser] ([Id], [Username], [Password], [UserType], [FKId]) VALUES (2, N'admin', N'admin', N'Admin', 0)
GO
INSERT [dbo].[PrivacyUser] ([Id], [Username], [Password], [UserType], [FKId]) VALUES (3, N'2', N'2', N'Trainer', 5)
GO
INSERT [dbo].[PrivacyUser] ([Id], [Username], [Password], [UserType], [FKId]) VALUES (4, N'3333', N'3333', N'Trainee', 7)
GO
INSERT [dbo].[PrivacyUser] ([Id], [Username], [Password], [UserType], [FKId]) VALUES (5, N'4444', N'4444', N'Trainer', 6)
GO
SET IDENTITY_INSERT [dbo].[PrivacyUser] OFF
GO
SET IDENTITY_INSERT [dbo].[Trainee] ON 
GO
INSERT [dbo].[Trainee] ([Id], [Gender], [Phone], [Interesting], [Location], [Email], [CreateDateTime]) VALUES (6, N'Femail', N'+6115838232222', N'Health,Education', N'1321321', N'2834400100@qq.com', CAST(N'2021-12-29T00:22:08.957' AS DateTime))
GO
INSERT [dbo].[Trainee] ([Id], [Gender], [Phone], [Interesting], [Location], [Email], [CreateDateTime]) VALUES (7, N'Femail', N'+6115838232222', N'Health,Education', N'1321321', N'2834400100@qq.com', CAST(N'2021-12-29T00:35:03.343' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Trainee] OFF
GO
SET IDENTITY_INSERT [dbo].[Trainer] ON 
GO
INSERT [dbo].[Trainer] ([Id], [Gender], [Phone], [KindOfTrainer], [Certificate], [DescribeYourself], [Location], [Email], [CreateDateTime]) VALUES (4, N'Femail', N'+6115838232222', N'Health,Education', N'2', N'555', N'555', N'2834400100@qq.com', CAST(N'2021-12-29T00:23:12.797' AS DateTime))
GO
INSERT [dbo].[Trainer] ([Id], [Gender], [Phone], [KindOfTrainer], [Certificate], [DescribeYourself], [Location], [Email], [CreateDateTime]) VALUES (5, N'Femail', N'+6115838232222', N'Health,Education', N'555', N'555', N'555', N'2834400100@qq.com', CAST(N'2021-12-29T00:26:18.170' AS DateTime))
GO
INSERT [dbo].[Trainer] ([Id], [Gender], [Phone], [KindOfTrainer], [Certificate], [DescribeYourself], [Location], [Email], [CreateDateTime]) VALUES (6, N'Femail', N'+6115838232222', N'Health,Education', N'555', N'555', N'555', N'2834400100@qq.com', CAST(N'2021-12-29T00:35:40.803' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Trainer] OFF
GO