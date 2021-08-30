USE [LicWebApi]
GO
/****** Object:  StoredProcedure [dbo].[GetAllLicenceData]    Script Date: 29-08-2021 18:39:56 ******/
DROP PROCEDURE [dbo].[GetAllLicenceData]
GO
/****** Object:  StoredProcedure [dbo].[DeleteLicence]    Script Date: 29-08-2021 18:39:56 ******/
DROP PROCEDURE [dbo].[DeleteLicence]
GO
/****** Object:  StoredProcedure [dbo].[CreateNewLicence]    Script Date: 29-08-2021 18:39:56 ******/
DROP PROCEDURE [dbo].[CreateNewLicence]
GO
/****** Object:  StoredProcedure [dbo].[CreateNewLicence]    Script Date: 29-08-2021 18:39:56 ******/
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
	set @ExpDt=null;
	if @Expirationdate<>''
	Begin
	set @ExpDt= (SELECT Convert(DATETIME, @Expirationdate, 103));
	end

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
/****** Object:  StoredProcedure [dbo].[DeleteLicence]    Script Date: 29-08-2021 18:39:56 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllLicenceData]    Script Date: 29-08-2021 18:39:56 ******/
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
	@SerchName int ,
	@Serachstring varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if @SerchName=0
	BEGIN
	SELECT Id,Licensekey,format(Expirationdate, 'dd/MM/yyyy') AS 'Expirationdate',CustomerName,CustomerEmail,IsDelete,format(Deletedate, 'dd/MM/yyyy') AS 'Deletedat' from LicenceMaster where ISNULL(IsDelete,0)=0 AND UPPER(Licensekey) like '%'+@Serachstring+'%'
	END
	else if @SerchName=1
	BEGIN
	SELECT Id,Licensekey,format(Expirationdate, 'dd/MM/yyyy') AS 'Expirationdate',CustomerName,CustomerEmail,IsDelete,format(Deletedate, 'dd/MM/yyyy') AS 'Deletedat' from LicenceMaster where ISNULL(IsDelete,0)=0 AND UPPER(Expirationdate) like '%'+@Serachstring+'%'
	END
	else if @SerchName=2
	BEGIN
	SELECT Id,Licensekey,format(Expirationdate, 'dd/MM/yyyy') AS 'Expirationdate',CustomerName,CustomerEmail,IsDelete,format(Deletedate, 'dd/MM/yyyy') AS 'Deletedat' from LicenceMaster where ISNULL(IsDelete,0)=0 AND UPPER(CustomerName) like '%'+@Serachstring+'%'
	END
	else if @SerchName=3
	BEGIN
	SELECT Id,Licensekey,format(Expirationdate, 'dd/MM/yyyy') AS 'Expirationdate',CustomerName,CustomerEmail,IsDelete,format(Deletedate, 'dd/MM/yyyy') AS 'Deletedat' from LicenceMaster where ISNULL(IsDelete,0)=0 AND UPPER(CustomerEmail) like '%'+@Serachstring+'%'
	END
	else if @SerchName=4
	BEGIN
	SELECT Id,Licensekey,format(Expirationdate, 'dd/MM/yyyy') AS 'Expirationdate',CustomerName,CustomerEmail,IsDelete,format(Deletedate, 'dd/MM/yyyy') AS 'Deletedat' from LicenceMaster where ISNULL(IsDelete,0)=0
	END

	--SELECT Id,Licensekey,Convert(DATETIME, Expirationdate, 103) AS 'Expirationdate',CustomerName,CustomerEmail,IsDelete,Convert(DATETIME, Deletedate, 103) AS 'Deletedat' from LicenceMaster where ISNULL(IsDelete,0)=0
END

GO
