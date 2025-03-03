USE [QuickDeal]
GO
/****** Object:  Table [dbo].[ad_categories]    Script Date: 01.02.25 11:11:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ad_categories](
	[ad_category_id] [int] IDENTITY(1,1) NOT NULL,
	[ad_category_name] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ad_category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ad_statuses]    Script Date: 01.02.25 11:11:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ad_statuses](
	[ad_status_id] [int] IDENTITY(1,1) NOT NULL,
	[ad_status_name] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ad_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ad_types]    Script Date: 01.02.25 11:11:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ad_types](
	[ad_type_id] [int] IDENTITY(1,1) NOT NULL,
	[ad_type_name] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ad_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ads]    Script Date: 01.02.25 11:11:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ads](
	[ad_id] [int] IDENTITY(1,1) NOT NULL,
	[publish_date] [datetime] NOT NULL,
	[city_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[ad_category_id] [int] NOT NULL,
	[ad_type_id] [int] NOT NULL,
	[ad_status_id] [int] NOT NULL,
	[title] [nvarchar](200) NOT NULL,
	[description] [nvarchar](1000) NOT NULL,
	[ad_price] [decimal](10, 2) NOT NULL,
	[ad_image] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ad_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 01.02.25 11:11:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cities](
	[city_id] [int] IDENTITY(1,1) NOT NULL,
	[city_region] [nvarchar](50) NOT NULL,
	[city_name] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[city_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 01.02.25 11:11:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[first_name] [nvarchar](70) NULL,
	[last_name] [nvarchar](70) NULL,
	[second_name] [nvarchar](70) NULL,
	[phone_number] [nvarchar](20) NULL,
	[profit] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ad_categories] ON 

INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (43, N'Автомобили')
INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (49, N'Детские товары')
INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (48, N'Книги')
INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (42, N'Мебель')
INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (44, N'Недвижимость')
INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (45, N'Одежда')
INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (50, N'Продукты питания')
INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (47, N'Спортивные товары')
INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (46, N'Техника')
INSERT [dbo].[ad_categories] ([ad_category_id], [ad_category_name]) VALUES (41, N'Электроника')
SET IDENTITY_INSERT [dbo].[ad_categories] OFF
GO
SET IDENTITY_INSERT [dbo].[ad_statuses] ON 

INSERT [dbo].[ad_statuses] ([ad_status_id], [ad_status_name]) VALUES (15, N'Активное')
INSERT [dbo].[ad_statuses] ([ad_status_id], [ad_status_name]) VALUES (16, N'Завершено')
SET IDENTITY_INSERT [dbo].[ad_statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[ad_types] ON 

INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (35, N'Аренда')
INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (42, N'Другое')
INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (43, N'Криптообмен')
INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (40, N'Купля')
INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (41, N'Лизинг')
INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (36, N'Обмен')
INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (34, N'Продажа')
INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (38, N'Работа')
INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (37, N'Сдача в аренду')
INSERT [dbo].[ad_types] ([ad_type_id], [ad_type_name]) VALUES (39, N'Услуги')
SET IDENTITY_INSERT [dbo].[ad_types] OFF
GO
SET IDENTITY_INSERT [dbo].[ads] ON 

INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (16, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 15, 35, 41, 34, 15, N'Продаю iPhone 12', N'Новый iPhone 12, 128GB, черный цвет.', CAST(50000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\QuickDeal\QuickDeal\References\Images\Ads\01.png')
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (17, CAST(N'2025-01-02T00:00:00.000' AS DateTime), 16, 36, 42, 35, 15, N'Продаю диван', N'Диван в хорошем состоянии, почти новый.', CAST(15000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\QuickDeal\QuickDeal\References\Images\Ads\02.png')
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (18, CAST(N'2025-01-03T00:00:00.000' AS DateTime), 17, 37, 43, 36, 16, N'Продажа авто', N'Машина в отличном состоянии, пробег 100000 км.', CAST(400000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\QuickDeal\QuickDeal\References\Images\Ads\03.png')
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (19, CAST(N'2025-01-04T00:00:00.000' AS DateTime), 18, 38, 44, 37, 15, N'Аренда квартиры', N'Сдается квартира на длительный срок, 2 комнаты.', CAST(25000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\QuickDeal\QuickDeal\References\Images\Ads\04.png')
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (20, CAST(N'2025-01-05T00:00:00.000' AS DateTime), 19, 39, 45, 38, 15, N'Продажа куртки', N'Зимняя куртка в отличном состоянии, размер L.', CAST(5000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\QuickDeal\QuickDeal\References\Images\Ads\05.png')
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (21, CAST(N'2025-01-06T00:00:00.000' AS DateTime), 20, 40, 46, 39, 16, N'Обмен ноутбука', N'Обменяю MacBook Pro на Samsung Galaxy.', CAST(70000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\QuickDeal\QuickDeal\References\Images\Ads\06.png')
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (22, CAST(N'2025-01-07T00:00:00.000' AS DateTime), 21, 41, 47, 40, 15, N'Сдача комнаты в аренду', N'Сдам комнату в центре города.', CAST(15000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\QuickDeal\QuickDeal\References\Images\Ads\07.png')
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (23, CAST(N'2025-01-08T00:00:00.000' AS DateTime), 22, 42, 48, 41, 15, N'Работа менеджером', N'Требуется менеджер по продажам.', CAST(30000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\QuickDeal\QuickDeal\References\Images\Ads\08.png')
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (24, CAST(N'2025-01-09T00:00:00.000' AS DateTime), 23, 43, 49, 42, 16, N'Продаю книги', N'Коллекция редких книг, 10 штук.', CAST(10000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\girl.jpg')
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (25, CAST(N'2025-01-10T00:00:00.000' AS DateTime), 24, 44, 50, 43, 15, N'Продажа спортивного инвентаря', N'Новый тренажер, почти не использовался.', CAST(25000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[ads] ([ad_id], [publish_date], [city_id], [user_id], [ad_category_id], [ad_type_id], [ad_status_id], [title], [description], [ad_price], [ad_image]) VALUES (31, CAST(N'2025-02-01T10:15:40.073' AS DateTime), 16, 46, 43, 35, 16, N'Птица', N'птицаптицаптицаптица', CAST(8000.00 AS Decimal(10, 2)), N'C:\Users\Daniel\Desktop\test.jpg')
SET IDENTITY_INSERT [dbo].[ads] OFF
GO
SET IDENTITY_INSERT [dbo].[cities] ON 

INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (15, N'Центральный', N'Москва')
INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (16, N'Южный', N'Краснодар')
INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (17, N'Сибирский', N'Новосибирск')
INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (18, N'Уральский', N'Екатеринбург')
INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (19, N'Дальневосточный', N'Владивосток')
INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (20, N'Приволжский', N'Казань')
INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (21, N'Западный', N'Уфа')
INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (22, N'Северный', N'Вологда')
INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (23, N'Кавказский', N'Пятигорск')
INSERT [dbo].[cities] ([city_id], [city_region], [city_name]) VALUES (24, N'Восточный', N'Хабаровск')
SET IDENTITY_INSERT [dbo].[cities] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (35, N'johndoe', N'password123', N'Иван', N'Петров', N'Александрович', N'1234567890', CAST(1000.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (36, N'janedoe', N'password456', N'Анна', N'Смирнова', N'Ивановна', N'0987654321', CAST(2000.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (37, N'marksmith', N'password789', N'Михаил', N'Кузнецов', N'Анатольевич', N'1122334455', CAST(1500.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (38, N'emilyjones', N'password321', N'Мария', N'Иванова', N'Сергеевна', N'5566778899', CAST(2500.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (39, N'robertbrown', N'password654', N'Сергей', N'Петров', N'Дмитриевич', N'6677889900', CAST(3000.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (40, N'susanwhite', N'password987', N'Ольга', N'Лебедева', N'Александровна', N'9988776655', CAST(1200.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (41, N'michaeltaylor', N'password159', N'Дмитрий', N'Васильев', N'Петрович', N'4433221100', CAST(1700.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (42, N'lindawilliams', N'password258', N'Татьяна', N'Соколова', N'Михайловна', N'2233445566', CAST(2300.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (43, N'jamesmiller', N'password753', N'Елена', N'Михайлова', N'Петровна', N'3344556677', CAST(2700.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (44, N'patriciajohnson', N'password963', N'Ирина', N'Дьякова', N'Юрьевна', N'1122334455', CAST(3500.00 AS Decimal(10, 2)))
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (45, N'Kelsy123', N'password123', N'Даниил', N'Даниилов', N'Даниилович', N'+7(999)999-99-99', NULL)
INSERT [dbo].[users] ([user_id], [login], [password], [first_name], [last_name], [second_name], [phone_number], [profit]) VALUES (46, N'TestUser1', N'password123', N'Петр', N'Петров', N'Петрович', N'+7(999)999-99-99', NULL)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ad_categ__4C7937D8494690EA]    Script Date: 01.02.25 11:11:44 ******/
ALTER TABLE [dbo].[ad_categories] ADD UNIQUE NONCLUSTERED 
(
	[ad_category_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ad_statu__92DFC22CC7947DD1]    Script Date: 01.02.25 11:11:44 ******/
ALTER TABLE [dbo].[ad_statuses] ADD UNIQUE NONCLUSTERED 
(
	[ad_status_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ad_types__6869F5D7EDFA80CB]    Script Date: 01.02.25 11:11:44 ******/
ALTER TABLE [dbo].[ad_types] ADD UNIQUE NONCLUSTERED 
(
	[ad_type_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__cities__1AA4F7B5283CC746]    Script Date: 01.02.25 11:11:44 ******/
ALTER TABLE [dbo].[cities] ADD UNIQUE NONCLUSTERED 
(
	[city_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__cities__2F8D6D90E04E54AC]    Script Date: 01.02.25 11:11:44 ******/
ALTER TABLE [dbo].[cities] ADD UNIQUE NONCLUSTERED 
(
	[city_region] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__users__7838F27277929F3E]    Script Date: 01.02.25 11:11:44 ******/
ALTER TABLE [dbo].[users] ADD UNIQUE NONCLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ads] ADD  DEFAULT (getdate()) FOR [publish_date]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ((0)) FOR [profit]
GO
ALTER TABLE [dbo].[ads]  WITH CHECK ADD FOREIGN KEY([ad_category_id])
REFERENCES [dbo].[ad_categories] ([ad_category_id])
GO
ALTER TABLE [dbo].[ads]  WITH CHECK ADD FOREIGN KEY([ad_status_id])
REFERENCES [dbo].[ad_statuses] ([ad_status_id])
GO
ALTER TABLE [dbo].[ads]  WITH CHECK ADD FOREIGN KEY([ad_type_id])
REFERENCES [dbo].[ad_types] ([ad_type_id])
GO
ALTER TABLE [dbo].[ads]  WITH CHECK ADD  CONSTRAINT [FK__ads__city_id__4AB81AF0] FOREIGN KEY([city_id])
REFERENCES [dbo].[cities] ([city_id])
GO
ALTER TABLE [dbo].[ads] CHECK CONSTRAINT [FK__ads__city_id__4AB81AF0]
GO
ALTER TABLE [dbo].[ads]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ads]  WITH CHECK ADD CHECK  (([ad_price]>=(0)))
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD CHECK  (([profit]>=(0)))
GO
