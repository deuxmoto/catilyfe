CREATE PROCEDURE auth.getuserinfo
    @error_message  NVARCHAR(2048) OUTPUT
   ,@id             INT           = NULL
   ,@email          NVARCHAR(256) = NULL
   ,@token          BINARY(64)    = NULL
AS
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT

    DECLARE @error          INT = 0

    IF(@id IS NULL)
    BEGIN

        SELECT TOP 1
           @id = u.id
        FROM auth.users u
        LEFT JOIN auth.tokens t
               ON t.userid = u.id AND t.expiry > GETUTCDATE()
        WHERE u.email = @email
           OR t.token = @token

    END

    -- OUTPUT all of the roles
    SELECT
        ur.userid
       ,r.role
    FROM auth.userroles ur
    JOIN auth.roles r
      ON r.id = ur.roleid
    WHERE ur.userid = @id
       OR @id IS NULL

    -- Output user details
    SELECT
        u.id
       ,u.name
       ,u.email
       ,u.pass
    FROM auth.users u
    WHERE u.id = @id
       OR @id IS NULL

ErrorHandler:
    RETURN @error
