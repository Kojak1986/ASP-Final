DROP TABLE orders
DROP TABLE items
DROP TABLE colours
DROP TABLE ItemFinish

CREATE TABLE dbo.ItemFinish
(
    FinishId nvarchar(128) NOT NULL DEFAULT (newid()),
	Name nvarchar(250) NOT NULL,
 CONSTRAINT [PK_FinishId] PRIMARY KEY CLUSTERED 
(
	[FinishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

);

INSERT INTO [dbo].[ItemFinish](Name) VALUES ('Glossy');
INSERT INTO [dbo].[ItemFinish](Name) VALUES ('Matte');

CREATE TABLE dbo.Colours
(
    ColourId nvarchar(128) NOT NULL DEFAULT (newid()),
	Name nvarchar(250) NOT NULL,
    CreateDate datetime NOT NULL DEFAULT((getUtcdate())),
    EditDate datetime NOT NULL DEFAULT((getUtcdate())),
 CONSTRAINT [PK_Colours] PRIMARY KEY CLUSTERED 
(
	[ColourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

);


CREATE TABLE dbo.Items
(
    ItemId nvarchar(128) NOT NULL DEFAULT (newid()),
	Name nvarchar(250) NOT NULL,
    CreateDate datetime NOT NULL DEFAULT((getUtcdate())),
    EditDate datetime NOT NULL DEFAULT((getUtcdate())),
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

);


CREATE TABLE dbo.Orders
(
    OrderId nvarchar(128) NOT NULL DEFAULT (newid()),
	CreateDate datetime NOT NULL DEFAULT((getUtcdate())),
    EditDate datetime NOT NULL DEFAULT((getUtcdate())),
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

ALTER TABLE [dbo].[Orders] ADD [FinishId] nvarchar(128) NOT NULL;

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_ItemFinish] FOREIGN KEY([FinishId])
REFERENCES [dbo].[ItemFinish] ([FinishId]);