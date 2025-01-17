USE [master]
GO
/****** Object:  Database [MiniBar]    Script Date: 5/2/2019 9:35:00 PM ******/
CREATE DATABASE [MiniBar]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MiniBar', FILENAME = N'c:\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\Data\MiniBar.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MiniBar_log', FILENAME = N'c:\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\Data\MiniBar_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MiniBar] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MiniBar].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MiniBar] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MiniBar] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MiniBar] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MiniBar] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MiniBar] SET ARITHABORT OFF 
GO
ALTER DATABASE [MiniBar] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MiniBar] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MiniBar] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MiniBar] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MiniBar] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MiniBar] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MiniBar] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MiniBar] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MiniBar] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MiniBar] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MiniBar] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MiniBar] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MiniBar] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MiniBar] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MiniBar] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MiniBar] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MiniBar] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MiniBar] SET RECOVERY FULL 
GO
ALTER DATABASE [MiniBar] SET  MULTI_USER 
GO
ALTER DATABASE [MiniBar] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MiniBar] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MiniBar] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MiniBar] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MiniBar] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MiniBar', N'ON'
GO
USE [MiniBar]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[BrandID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[ImageID] [int] NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoryNames]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoryNames](
	[CategoryID] [int] NOT NULL,
	[LanguageID] [varchar](10) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_CategoryNames] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC,
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdentityRole]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRole](
	[Id] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_IdentityRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityRoleClaim`1]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRoleClaim`1](
	[Id] [int] NOT NULL,
	[RoleId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserClaim`1]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserClaim`1](
	[Id] [int] NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserLogin`1]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserLogin`1](
	[LoginProvider] [nvarchar](max) NULL,
	[ProviderKey] [nvarchar](max) NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserRole`1]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserRole`1](
	[UserId] [nvarchar](max) NULL,
	[RoleId] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserToken`1]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserToken`1](
	[UserId] [nvarchar](max) NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Images]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Languages]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Languages](
	[LanguageID] [varchar](10) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductInfo]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductInfo](
	[ProductID] [int] NOT NULL,
	[LanguageID] [varchar](10) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](2000) NULL,
 CONSTRAINT [PK_ProductInfo] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[BrandID] [int] NOT NULL,
	[Size] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[ImageID] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[NormalizedUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[NormalizedEmail] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (1, N'Absolut', 4)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (2, N'Smirnoff', 3)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (3, N'Hennessy', 2)
SET IDENTITY_INSERT [dbo].[Brands] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (21, NULL)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (22, NULL)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (23, 21)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (24, 22)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (25, NULL)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (26, 25)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (27, 22)
SET IDENTITY_INSERT [dbo].[Categories] OFF
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (21, N'en', N'Food')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (21, N'it', N'Cibo')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (22, N'en', N'Drinks')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (22, N'it', N'Bevanda')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (23, N'en', N'Snacks')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (23, N'it', N'Spuntini')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (24, N'en', N'Vodka')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (24, N'it', N'Vodka')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (25, N'en', N'Kids')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (25, N'it', N'Bambine')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (26, N'en', N'Milk')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (26, N'it', N'Latte')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (27, N'en', N'Cognac')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (27, N'it', N'Cognac')
SET IDENTITY_INSERT [dbo].[Images] ON 

INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (2, N'assets\images\3b22538b-c980-4cce-a35c-380d07832b2c.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (3, N'assets\images\3521e82a-ef4a-462f-bdce-4e4c9c95a087.png')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (4, N'assets\images\619a99d7-151d-457b-a132-c6d11efaf241.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (6, N'assets\images\17a8d375-a724-4308-a528-711f825dc3e4.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (7, N'assets\images\0f2d7e6a-4636-4276-b8e3-cabb19142674.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (8, N'assets\images\0cbdef99-9cc1-4321-848e-c13919acd397.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (9, N'assets\images\1dc204c5-7a85-457d-bc2b-5b1f1d446259.png')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (10, N'assets\images\6f0e4997-8818-484d-9864-29352b763be9.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (11, N'assets\images\b72239fa-7f78-49e8-9348-a536807a7dbc.jpg')
SET IDENTITY_INSERT [dbo].[Images] OFF
INSERT [dbo].[Languages] ([LanguageID], [Name]) VALUES (N'en', N'English')
INSERT [dbo].[Languages] ([LanguageID], [Name]) VALUES (N'it', N'Italian')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (22, N'en', N'Absolut Original Vodka', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (22, N'it', N'Absolut Original Vodka', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (23, N'en', N'Absolut Raspberri Vodka', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (23, N'it', N'Absolut Raspberri Vodka', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (24, N'en', N'Smirnoff Vodka No. 21', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (24, N'it', N'Smirnoff Vodka No. 21', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (25, N'en', N'Smirnoff Vodka 1L', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (25, N'it', N'Smirnoff Vodka 1L', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (26, N'en', N'Hennessy XO Cognac', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (26, N'it', N'Hennessy XO Cognac', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (27, N'en', N'Hennessy V.S.O.P. Privilège', N'Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (27, N'it', N'Hennessy V.S.O.P. Privilège', N'Desc')
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (22, 24, 1, 1, 6.9899997711181641, 6)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (23, 24, 1, 1, 6.9899997711181641, 7)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (24, 24, 2, 1, 4.9899997711181641, 8)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (25, 24, 2, 1, 8.9899997711181641, 9)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (26, 27, 3, 1, 30, 10)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (27, 27, 3, 1, 25, 11)
SET IDENTITY_INSERT [dbo].[Products] OFF
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b56cc577-929b-4402-8a4e-fc904316f074', N'admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAELzyq0RkzixlSF5dT7vzD1l2ZJS/C0uEJtshCJmBXIa2iet+HIAJyK5VRjCYGc791A==', N'YS5WKLTEYCTWK7OUZLCUZMPHG2DGWXPL', N'5ed9e67d-cc34-46f4-91e6-1c52578dbf93', NULL, 0, 0, NULL, 1, 0)
ALTER TABLE [dbo].[Brands]  WITH CHECK ADD  CONSTRAINT [FK_Brands_Images] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Images] ([ImageID])
GO
ALTER TABLE [dbo].[Brands] CHECK CONSTRAINT [FK_Brands_Images]
GO
ALTER TABLE [dbo].[CategoryNames]  WITH NOCHECK ADD  CONSTRAINT [FK_CategoryNames_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[CategoryNames] CHECK CONSTRAINT [FK_CategoryNames_Categories]
GO
ALTER TABLE [dbo].[CategoryNames]  WITH NOCHECK ADD  CONSTRAINT [FK_CategoryNames_Languages] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Languages] ([LanguageID])
GO
ALTER TABLE [dbo].[CategoryNames] CHECK CONSTRAINT [FK_CategoryNames_Languages]
GO
ALTER TABLE [dbo].[ProductInfo]  WITH CHECK ADD  CONSTRAINT [FK_ProductInfo_Languages] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Languages] ([LanguageID])
GO
ALTER TABLE [dbo].[ProductInfo] CHECK CONSTRAINT [FK_ProductInfo_Languages]
GO
ALTER TABLE [dbo].[ProductInfo]  WITH CHECK ADD  CONSTRAINT [FK_ProductInfo_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[ProductInfo] CHECK CONSTRAINT [FK_ProductInfo_Products]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Brands] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brands] ([BrandID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Images] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Images] ([ImageID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Images]
GO
/****** Object:  StoredProcedure [dbo].[GetCategoryBrands]    Script Date: 5/2/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCategoryBrands]
	@categoryID [int] = 0
AS
SELECT DISTINCT Brands.* FROM dbo.Products
	LEFT JOIN dbo.Brands
		ON Brands.BrandID = Products.BrandID
WHERE Products.CategoryID = @categoryID

GO
USE [master]
GO
ALTER DATABASE [MiniBar] SET  READ_WRITE 
GO
