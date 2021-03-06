USE [LicWebApi]
GO
/****** Object:  Table [dbo].[LicenceMaster]    Script Date: 26-08-2021 20:59:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenceMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Licensekey] [nvarchar](50) NULL,
	[Expirationdate] [datetime] NULL,
	[CustomerName] [nvarchar](200) NULL,
	[CustomerEmail] [nvarchar](100) NULL,
	[IsDelete] [bit] NULL,
	[Deletedate] [datetime] NULL,
 CONSTRAINT [PK_LicenceMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[CreateNewLicence]    Script Date: 26-08-2021 20:59:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateNewLicence] 
	-- Add the parameters for the stored procedure here
	@Licensekey varchar(50),
	@Expirationdate varchar(50),
	@CustomerName varchar(100),
	@CustomerEmail varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @ExpDt datetime;
	set @ExpDt= (SELECT Convert(DATETIME, @Expirationdate, 103));

    -- Insert statements for procedure here
	INSERT INTO LicenceMaster
           (Licensekey
           ,Expirationdate
           ,CustomerName
           ,CustomerEmail
           ,IsDelete
           ,Deletedate)
     VALUES
           (@Licensekey
           ,@ExpDt
           ,@CustomerName
           ,@CustomerEmail
           ,0
           ,'') 

		   select SCOPE_IDENTITY() As 'Id'
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteLicence]    Script Date: 26-08-2021 20:59:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteLicence] 
	-- Add the parameters for the stored procedure here
	@Id int,
	@Deletedate varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @DelDt datetime;
	set @DelDt= (SELECT Convert(DATETIME, @Deletedate, 103));

    -- Insert statements for procedure here
	Update LicenceMaster set IsDelete=1,Deletedate=@DelDt where Id=@Id

	select 'Success' As 'Result'

END

GO
/****** Object:  StoredProcedure [dbo].[GetAllLicenceData]    Script Date: 26-08-2021 20:59:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllLicenceData] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id,Licensekey,Convert(DATETIME, Expirationdate, 103) AS 'Expirationdate',CustomerName,CustomerEmail,IsDelete,Convert(DATETIME, Deletedate, 103) AS 'Deletedat' from LicenceMaster where ISNULL(IsDelete,0)=0
END

GO
