/****** Object:  Table [dbo].[tblAfterJackMain]    Script Date: 5/19/2015 4:55:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblAfterJackMain](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EntryDate] [datetime] NOT NULL,
	[Crew] [nchar](10) NOT NULL,
	[Room] [nchar](10) NOT NULL,
	[Pot] [int] NOT NULL,
 CONSTRAINT [PK_tblAfterJackMain] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[tblAfterJackDetail]    Script Date: 5/19/2015 4:57:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblAfterJackDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AfterJackID] [int] NOT NULL,
	[AnodeNum] [int] NOT NULL,
	[ClampDrop] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_tblAfterJackDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
