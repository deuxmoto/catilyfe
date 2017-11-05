CREATE PROCEDURE auth.setrole
    @error_message NVARCHAR(2048) OUTPUT
   ,@name          NVARCHAR(64)
   ,@description   NVARCHAR(128)
   ,@revive        BIT = 1
AS
    
    SET NOCOUNT ON

    BEGIN TRY
    DECLARE @error INT = 0
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    BEGIN TRANSACTION

    MERGE auth.roles r
    USING (VALUES (@name, @description)) src(name, description)
       ON src.name = r.role
    WHEN MATCHED THEN
      UPDATE
         SET r.description = src.description
            ,r.isdeleted = CASE WHEN @revive = 1 THEN 0 ELSE r.isdeleted END
    WHEN NOT MATCHED THEN
      INSERT (role, description) VALUES (src.name, src.description);

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

