USE [PRIDEMOSINF]
GO

/****** Object:  Table [dbo].[PickingList]    Script Date: 12-12-2016 16:48:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PickingList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EstadoTratado] [bit] NOT NULL,
	[idLinha] [uniqueidentifier] NOT NULL,
	[IdCabecDoc] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PickingList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PickingList]  WITH CHECK ADD  CONSTRAINT [FK_PickingList_CabecDoc] FOREIGN KEY([IdCabecDoc])
REFERENCES [dbo].[CabecDoc] ([Id])
GO

ALTER TABLE [dbo].[PickingList] CHECK CONSTRAINT [FK_PickingList_CabecDoc]
GO

ALTER TABLE [dbo].[PickingList]  WITH CHECK ADD  CONSTRAINT [FK_PickingList_LinhasDoc] FOREIGN KEY([idLinha])
REFERENCES [dbo].[LinhasDoc] ([Id])
GO

ALTER TABLE [dbo].[PickingList] CHECK CONSTRAINT [FK_PickingList_LinhasDoc]
GO

