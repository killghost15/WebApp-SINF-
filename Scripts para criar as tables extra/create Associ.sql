USE [PRIDEMOSINF]
GO

/****** Object:  Table [dbo].[AssocPickingList_PickingWave]    Script Date: 13-12-2016 20:59:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AssocPickingList_PickingWave](
	[idPickingList] [int] NOT NULL,
	[idPickingWave] [int] NOT NULL,
 CONSTRAINT [IX_AssocPickingList_PickingWave] UNIQUE NONCLUSTERED 
(
	[idPickingList] ASC,
	[idPickingWave] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AssocPickingList_PickingWave]  WITH CHECK ADD  CONSTRAINT [FK_AssocPickingList_PickingWave_PickingWave] FOREIGN KEY([idPickingWave])
REFERENCES [dbo].[PickingWave] ([Id])
GO

ALTER TABLE [dbo].[AssocPickingList_PickingWave] CHECK CONSTRAINT [FK_AssocPickingList_PickingWave_PickingWave]
GO

