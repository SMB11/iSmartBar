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
SET IDENTITY_INSERT [dbo].[Categories] OFF
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (21, N'en', N'Food')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (21, N'it', N'Cibo')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (22, N'en', N'Drinks')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (22, N'it', N'Bevanda')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (23, N'en', N'Snacks')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (23, N'it', N'Spuntini')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (24, N'en', N'Alcoholic')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (24, N'it', N'Alcolizzata')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (25, N'en', N'Kids')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (25, N'it', N'Bambine')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (26, N'en', N'Milk')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (26, N'it', N'Latte')
SET IDENTITY_INSERT [dbo].[Images] ON 

INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (1, N'assets\images\15ddb639-3055-4f1d-9e1f-5f56694a38f5.bmp')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (2, N'assets\images\3b22538b-c980-4cce-a35c-380d07832b2c.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (3, N'assets\images\3521e82a-ef4a-462f-bdce-4e4c9c95a087.png')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (4, N'assets\images\619a99d7-151d-457b-a132-c6d11efaf241.jpg')
INSERT [dbo].[Images] ([ImageID], [Path]) VALUES (5, N'assets\images\b5280ef4-cd26-4fca-adab-c2fc987b1338.jpg')
SET IDENTITY_INSERT [dbo].[Images] OFF
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (1, N'Coca Cola', 4)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (2, N'Pepsi', 3)
INSERT [dbo].[Brands] ([BrandID], [Name], [ImageID]) VALUES (3, N'Snickers', 2)
SET IDENTITY_INSERT [dbo].[Brands] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (19, 24, 1, 0, 2.9900000095367432, 1)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price], [ImageID]) VALUES (21, 23, 3, 0, 1.9900000095367432, 5)
SET IDENTITY_INSERT [dbo].[Products] OFF
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (19, N'en', N'Coca Cola 0.5', N'asdasdasdasd')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (19, N'it', N'Coca Cola 0.5', N'asdasd')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (21, N'en', N'Snickers Chocolate Bar', N'asd')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (21, N'it', N'Snickers Barretta di cioccolato', N'asdasd')
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b56cc577-929b-4402-8a4e-fc904316f074', N'admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAELzyq0RkzixlSF5dT7vzD1l2ZJS/C0uEJtshCJmBXIa2iet+HIAJyK5VRjCYGc791A==', N'YS5WKLTEYCTWK7OUZLCUZMPHG2DGWXPL', N'5ed9e67d-cc34-46f4-91e6-1c52578dbf93', NULL, 0, 0, NULL, 1, 0)
