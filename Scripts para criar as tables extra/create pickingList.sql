USE [PRIDEMOSINF]
GO

/****** Object:  Table [dbo].[PickingList]    Script Date: 12-12-2016 23:10:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PickingList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Artigo] [nvarchar](48) NOT NULL,
	[Localizacao] [varchar](30) NOT NULL,
	[Quantidade] [int] NOT NULL,
	[EstadoTratado] [bit] NOT NULL,
	[IdECL] [uniqueidentifier] NOT NULL,
	[idLinha] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PickingList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PickingList]  WITH CHECK ADD  CONSTRAINT [FK_PickingList_ArmazemLocalizacoes] FOREIGN KEY([Localizacao])
REFERENCES [dbo].[ArmazemLocalizacoes] ([Localizacao])
GO

ALTER TABLE [dbo].[PickingList] CHECK CONSTRAINT [FK_PickingList_ArmazemLocalizacoes]
GO

ALTER TABLE [dbo].[PickingList]  WITH CHECK ADD  CONSTRAINT [FK_PickingList_Artigo] FOREIGN KEY([Artigo])
REFERENCES [dbo].[Artigo] ([Artigo])
GO

ALTER TABLE [dbo].[PickingList] CHECK CONSTRAINT [FK_PickingList_Artigo]
GO

ALTER TABLE [dbo].[PickingList]  WITH CHECK ADD  CONSTRAINT [FK_PickingList_CabecDoc] FOREIGN KEY([IdECL])
REFERENCES [dbo].[CabecDoc] ([Id])
GO

ALTER TABLE [dbo].[PickingList] CHECK CONSTRAINT [FK_PickingList_CabecDoc]
GO

ALTER TABLE [dbo].[PickingList]  WITH CHECK ADD  CONSTRAINT [FK_PickingList_LinhasDoc] FOREIGN KEY([idLinha])
REFERENCES [dbo].[LinhasDoc] ([Id])
GO

ALTER TABLE [dbo].[PickingList] CHECK CONSTRAINT [FK_PickingList_LinhasDoc]
GO

