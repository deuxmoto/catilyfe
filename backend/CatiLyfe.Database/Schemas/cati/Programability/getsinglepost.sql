-- This gets a single post

CREATE PROCEDURE cati.getsinglepost
    @error_message NVARCHAR(2048) OUTPUT
   ,@id             INT = NULL
   ,@slug           NVARCHAR(256) = NULL
   ,@isadmin        BIT = 0
AS
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT

    DECLARE @error INT = 0

    IF(@id IS NULL)
    BEGIN
        SET @id = (SELECT id FROM cati.postmeta WHERE slug = @slug)

        IF(@id IS NULL)
        BEGIN
            SET @error = 50001
            SET @error_message = N'The post was not found with slug ''' + ISNULL(@slug, 'NULL') + ''''
            GOTO ErrorHandler
        END
    END

    IF(NOT EXISTS(SELECT TOP 1 1 FROM cati.postmeta WHERE id = @id))
    BEGIN
        SET @error_message = N'The post was not found with id = ''' + ISNULL(CAST(@id AS NVARCHAR), 'NULL') + ''''
        SET @error = 50001
        GOTO ErrorHandler
    END

    SELECT
        m.title
       ,m.slug
       ,m.id
       ,m.created
       ,m.goeslive
       ,m.description
    FROM cati.postmeta m
    WHERE (id = @id OR slug = @slug)
        AND (ispublished = 1 OR @isadmin = 1)

    SELECT
        p.id
       ,p.postid
       ,p.type
       ,p.content
    FROM cati.postcontent p
    WHERE p.postid = @id

    -- Find the tags
    SELECT
       @id AS post
      ,t.tag
    FROM cati.posttags pt
    JOIN cati.tags t
      ON t.id = pt.tag
    WHERE pt.post = @id

ErrorHandler:
    RETURN @error
