CREATE PROCEDURE cati.deletepost
    @error_message NVARCHAR(2048) OUTPUT
   ,@id            INT = NULL
   ,@slug          NVARCHAR(256) = NULL
AS
    SET NOCOUNT ON

    DECLARE @itemnotFound INT = 50001
    DECLARE @error INT = 0

    SET TRANSACTION ISOLATION LEVEL READ COMMITTED

    BEGIN TRY
    BEGIN TRANSACTION

    IF(@id IS NULL)
    BEGIN
        SET @id = (SELECT id FROM cati.postmeta WHERE slug = @slug)

        IF(@id IS NULL)
        BEGIN
            SET @error = @itemnotfound
            SET @error_message = N'The post was not found with slug ''' + ISNULL(@slug, 'NULL') + ''''
            GOTO ErrorHandler
        END
    END

    IF(NOT EXISTS(SELECT TOP 1 1 FROM cati.postmeta WHERE id = @id))
    BEGIN
        SET @error_message = N'The post was not found with id = ''' + ISNULL(CAST(@id AS NVARCHAR), 'NULL') + ''''
        SET @error = @itemnotfound
        GOTO ErrorHandler
    END

    UPDATE cati.postmeta
        SET isdeleted = 1
    WHERE id = @id

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
