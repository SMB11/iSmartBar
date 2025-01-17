USE [MiniBar]
GO
INSERT [dbo].[Languages] ([LanguageID], [Name]) VALUES (N'en', N'English')
INSERT [dbo].[Languages] ([LanguageID], [Name]) VALUES (N'it', N'Italian')
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
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (1, N'Absolut', 4)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (2, N'Smirnoff', 3)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (3, N'Hennessy', 2)
SET IDENTITY_INSERT [dbo].[Brands] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (22, 24, 1, 1, 6.9899997711181641, 6)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (23, 24, 1, 1, 6.9899997711181641, 7)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (24, 24, 2, 1, 4.9899997711181641, 8)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (25, 24, 2, 1, 8.9899997711181641, 9)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (26, 27, 3, 1, 30, 10)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (27, 27, 3, 1, 25, 11)
SET IDENTITY_INSERT [dbo].[Products] OFF
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
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b56cc577-929b-4402-8a4e-fc904316f074', N'admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAELzyq0RkzixlSF5dT7vzD1l2ZJS/C0uEJtshCJmBXIa2iet+HIAJyK5VRjCYGc791A==', N'YS5WKLTEYCTWK7OUZLCUZMPHG2DGWXPL', N'5ed9e67d-cc34-46f4-91e6-1c52578dbf93', NULL, 0, 0, NULL, 1, 0)
