CREATE PROCEDURE auth.setuserinfo
    @error_message  NVARCHAR(2048) OUTPUT
   ,@id             INT           = NULL
   ,@name           NVARCHAR(128) = NULL
   ,@email          NVARCHAR(256) = NULL
   ,@password       BINARY(64)    = NULL
   ,@rolelist       auth.rolelist READONLY
AS
    SET NOCOUNT ON

    DECLARE @notfound       INT = 50001
    DECLARE @invalidArgs    INT = 50002
    DECLARE @duplicateitem  INT = 50003

    BEGIN TRY
    DECLARE @error INT = 0
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    BEGIN TRANSACTION

    IF (@id IS NULL)
    BEGIN
        IF (@email IS NULL OR @password IS NULL OR @name IS NULL)
        BEGIN
            SET @error = @invalidArgs
            SET @error_message = N'The arguments for creating a new user are invalid'
            GOTO ErrorHandler
        END

        IF (EXISTS (SELECT TOP 1 1 FROM auth.users WHERE email = @email))
        BEGIN
            SET @error = @duplicateitem
            SET @error_message = N'The email already exists.'
            GOTO ErrorHandler
        END

        INSERT INTO auth.users
        (
            name
           ,email
           ,pass
        )
        VALUES
        (
            @name
           ,@email
           ,@password
        )

        SET @id = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN

        UPDATE auth.users
        SET name = ISNULL(@name, name)
           ,email = ISNULL(@email, email)
           ,pass = ISNULL(@password, pass)
        WHERE id = @id

    END

    IF (EXISTS( SELECT TOP 1 1 FROM @rolelist))
    BEGIN

        MERGE auth.userroles roles
        USING (SELECT def.id FROM @rolelist r
                JOIN auth.roles def
                  ON def.role = r.role
        ) AS r
           ON roles.userid = @id AND r.id = roles.roleid
        WHEN NOT MATCHED BY TARGET
        THEN INSERT
        (
            userid
           ,roleid
        )
        VALUES
        (
            @id
           ,r.id
        )
        WHEN NOT MATCHED BY SOURCE AND roles.userid = @id
        THEN DELETE;

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
