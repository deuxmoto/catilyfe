CREATE PROCEDURE auth.deleterole
    @error_message NVARCHAR(2048) OUTPUT
   ,@name          NVARCHAR(64)
   ,@commit        BIT
AS
    
    SET NOCOUNT ON

    DECLARE @notfound INT = 50001

    BEGIN TRY
    DECLARE @error INT = 0
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    BEGIN TRANSACTION

    DECLARE @id INT = NULL

    SELECT @id = r.role FROM auth.roles r WHERE r.role = @name

    IF(@id IS NULL)
    BEGIN
        SET @error = @notfound
        SET @error_message = N'The post with name ''' + @name + ''' was not found';
        GOTO ErrorHandler
    END

    IF (@commit = 1)
    BEGIN
        DELETE FROM auth.roles
        WHERE id = @id

        DELETE FROM auth.userroles
        WHERE roleid = @id
    END
    ELSE
    BEGIN
        UPDATE auth.roles
           SET isdeleted = 1
        WHERE id = @id
    END

    COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        SET @error = ERROR_NUMBER()
        SET @error_message = ERROR_MESSAGE()
        GOTO ErrorHandler
    END CATCH
RETURN @error

ErrorHandler:
    ROLLBACK TRANSACTION
RETURN @error