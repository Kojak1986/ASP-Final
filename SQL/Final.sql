CREATE TABLE dbo.Colours
(
    ColourId nvarchar(128) NOT NULL DEFAULT (newid()),
	Name nvarchar(250) NOT NULL,
	Type nvarchar(128) NOT NULL,
    CreateDate datetime NOT NULL DEFAULT((getdate())),
    EditDate datetime NOT NULL DEFAULT((getdate())),
 CONSTRAINT [PK_Colours] PRIMARY KEY CLUSTERED 
(
	[ColourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

);


CREATE TABLE dbo.Items
(
    ItemId nvarchar(128) NOT NULL DEFAULT (newid()),
	Name nvarchar(250) NOT NULL,
	ColourId nvarchar(128) NOT NULL,
    CreateDate datetime NOT NULL DEFAULT((getdate())),
    EditDate datetime NOT NULL DEFAULT((getdate())),
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

);


ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Colours] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colours] ([ColourId]);

CREATE TABLE dbo.Orders
(
    OrderId nvarchar(128) NOT NULL DEFAULT (newid()),
    CreateDate datetime NOT NULL DEFAULT((getdate())),
    EditDate datetime NOT NULL DEFAULT((getdate())),
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

);


ALTER TABLE [dbo].[Orders] ADD [ItemId] nvarchar(128) NOT NULL;

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Items] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([ItemId]);


ALTER TABLE [dbo].[Orders] ADD [ColourId] nvarchar(128) NOT NULL;

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Colours] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colours] ([ColourId]);