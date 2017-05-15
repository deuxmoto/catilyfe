CREATE PROCEDURE cati.setpost
    @error_message NVARCHAR(2048) OUTPUT
   ,@id            INT
   ,@slug          NVARCHAR(256)
   ,@title         NVARCHAR(256)
   ,@description   NVARCHAR(256)
   ,@goeslive      DATETIME2
   ,@content       cati.postcontent READONLY
AS
    SET NOCOUNT ON

    BEGIN TRY
    DECLARE @error INT = 0
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    BEGIN TRANSACTION

    MERGE INTO cati.postmeta m
    USING (SELECT @slug AS slug, @title AS title, @description AS description, @goeslive AS goeslive, @id AS id) AS src
    ON m.id = src.id
    WHEN MATCHED THEN
        UPDATE SET
            m.slug = src.slug
           ,m.title = src.title
           ,m.description = src.description
           ,m.goeslive = src.goeslive
    WHEN NOT MATCHED THEN
        INSERT
        (
            slug
           ,title
           ,description
           ,goeslive
        )
        VALUES
        (
            src.slug
           ,src.title
           ,src.description
           ,src.goeslive
        );

    SET @id = ISNULL(@id, SCOPE_IDENTITY())

    MERGE INTO cati.postcontent c
    USING @content src
       ON c.postid = @id AND c.id = src.id
    WHEN MATCHED THEN
        UPDATE SET
           type = src.type
          ,content = src.content
    WHEN NOT MATCHED THEN
        INSERT
        (
            id
           ,type
           ,content
        )
        VALUES
        (
            src.id
           ,src.type
           ,src.content
        )
    WHEN NOT MATCHED BY SOURCE AND c.postid = @id
        THEN DELETE;

    COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        GOTO ErrorHandler
    END CATCH
RETURN @error

ErrorHandler:
    SET @error_message = ISNULL(@error_message, ERROR_MESSAGE())
RETURN @error

