use [Payroll_Service]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE UpdateDetails

	@ID int,
	@Name varchar(20),
	@BasicPay bigint
AS
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @new_identity INTEGER = 0;
	DECLARE @result bit = 0;
	UPDATE employee_payroll set BasicPay = @BasicPay where ID = @ID and Name = @Name
	COMMIT TRANSACTION
	SET @result = 1;
	RETURN @result;
	END TRY
	BEGIN CATCH

	IF(XACT_STATE()) = -1
		BEGIN
		PRINT
		'Transaction is uncommitable' + ' Rolling back transaction'
		ROLLBACK TRANSACTION;
		RETURN @result;		
		END
	ELSE IF(XACT_STATE()) = 1
		BEGIN
		PRINT
		'Transaction is commitable' + ' Commiting back transaction'
		COMMIT TRANSACTION;
		SET @result = 1;
	    RETURN @result;
	END;
	END CATCH
END
GO
