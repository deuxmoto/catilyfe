CREATE PROCEDURE auth.settoken
    @error_message NVARCHAR(2048) OUTPUT
   ,@userid        INT          = NULL
   ,@token         BINARY(64)   = NULL
   ,@expiry        DATETIME2    = NULL
AS
    
    SET NOCOUNT ON

    DECLARE @notfound       INT = 50001
    DECLARE @invalidArgs    INT = 50002
    DECLARE @duplicateitem  INT = 50003

    BEGIN TRY
    DECLARE @error INT = 0
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    BEGIN TRANSACTION

    DECLARE @tokenId    INT = NULL

    IF (@token IS NULL)
    BEGIN
        SET @error = @invalidArgs
        SET @error_message = N'The token cannot be null'
        GOTO ErrorHandler
    END

    SELECT @tokenId = tokenid
    FROM auth.tokens
    WHERE token = @token

    IF (@tokenId IS NULL)
    BEGIN
        INSERT INTO auth.tokens
        (
            userid
           ,token
           ,expiry
        )
        VALUES
        (
            @userid
           ,@token
           ,@expiry
        )
    END
    ELSE
    BEGIN
        UPDATE auth.tokens
        SET expiry = @expiry
        WHERE token = @token
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

