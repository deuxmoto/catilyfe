CREATE PROCEDURE auth.getuserinfo
    @error_message  NVARCHAR(2048) OUTPUT
   ,@id             INT           = NULL
   ,@email          NVARCHAR(256) = NULL
   ,@token          BINARY(64)    = NULL
AS
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT

    DECLARE @error          INT = 0

        SELECT TOP 1
           u.id
          ,u.email
          ,u.name
          ,u.pass
        FROM auth.users u
        LEFT JOIN auth.tokens t
               ON t.userid = u.id AND t.expiry > GETUTCDATE()
        WHERE u.id = @id
           OR u.email = @email
           OR t.token = @token

ErrorHandler:
    RETURN @error
