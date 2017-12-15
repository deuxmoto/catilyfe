CREATE PROCEDURE cati.setpost
    @error_message NVARCHAR(2048) OUTPUT
   ,@id            INT = NULL
   ,@slug          NVARCHAR(256)
   ,@title         NVARCHAR(256)
   ,@description   NVARCHAR(256)
   ,@goeslive      DATETIME2
   ,@userid        INT
   ,@ispublished   BIT
   ,@isreserved    BIT
   ,@revision      INT
   ,@content       cati.postcontentlist READONLY
   ,@tags          cati.tagslist        READONLY
AS
    SET NOCOUNT ON

    DECLARE @notFound           INT = 50001
    DECLARE @invalidArgs        INT = 50002
    DECLARE @revisionConflict   INT = 50004

    DECLARE @auditModify NVARCHAR(64) = 'Modified'
    DECLARE @auditCreate NVARCHAR(64) = 'Created'

    BEGIN TRY

    DECLARE @error INT = 0
    DECLARE @auditOperation NVARCHAR(64) = NULL

    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    BEGIN TRANSACTION

    IF(EXISTS (SELECT TOP 1 1 FROM cati.postmeta WHERE slug = @slug AND (@id IS NULL OR @id <> id)) OR @slug IS NULL)
    BEGIN
        SET @error = @invalidArgs
        SET @error_message = N'The slug ''' + ISNULL(@slug, 'NULL') + ''' is not available.'
        GOTO ErrorHandler
    END

    IF(@id IS NOT NULL AND NOT EXISTS (SELECT TOP 1 1 FROM cati.postmeta WHERE id = @id))
    BEGIN
        SET @error = @notFound
        SET @error_message = N'Post with id ''' + CAST(@id AS NVARCHAR(10)) + ''' does not exist'
        GOTO ErrorHandler
    END

    IF(@id IS NOT NULL AND NOT EXISTS (SELECT TOP 1 1 FROM cati.postmeta WHERE id = @id AND revision = @revision))
    BEGIN
        SET @error = @revisionConflict
        SET @error_message = N'The revision id does not match.'
        GOTO ErrorHandler
    END

    MERGE INTO cati.postmeta m
    USING (SELECT @slug AS slug, @title AS title, @description AS description, @goeslive AS goeslive, @id AS id, @ispublished AS ispublished, @isreserved AS isreserved) AS src
       ON m.id = src.id
    WHEN MATCHED THEN
        UPDATE SET
            m.slug = src.slug
           ,m.title = src.title
           ,m.description = src.description
           ,m.goeslive = src.goeslive
           ,m.ispublished = src.ispublished
           ,m.isreserved = src.isreserved
           ,m.revision = m.revision + 1
    WHEN NOT MATCHED THEN
        INSERT
        (
            slug
           ,title
           ,description
           ,goeslive
           ,created
           ,ispublished
           ,isreserved
        )
        VALUES
        (
            src.slug
           ,src.title
           ,src.description
           ,src.goeslive
           ,GETUTCDATE()
           ,@ispublished
           ,@isreserved
        );

    IF(@id IS NULL)
    BEGIN
        SET @auditOperation = @auditCreate
    END
    ELSE
    BEGIN
        SET @auditOperation = @auditModify
    END

    SET @id = ISNULL(@id, SCOPE_IDENTITY())

    -- Update the audit log with the operation.
    INSERT INTO cati.postaudit
    (
        postid
       ,userid
       ,action
    )
    VALUES
    (
        @id
       ,@userid
       ,@auditOperation
    )

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
           ,postid
           ,type
           ,content
        )
        VALUES
        (
            src.id
           ,@id
           ,src.type
           ,src.content
        )
    WHEN NOT MATCHED BY SOURCE AND c.postid = @id
        THEN DELETE;

    DECLARE @tagIds TABLE
    (
        id INT
       ,tag NVARCHAR(64)
    )

    INSERT INTO @tagIds
    SELECT
        tags.id
       ,tags.tag
    FROM @tags input
    JOIN cati.tags tags
      ON input.tag = tags.tag

    INSERT INTO cati.tags
    (
        tag
    )
    OUTPUT
      inserted.id
     ,inserted.tag
    INTO @tagIds
    SELECT
       input.tag
    FROM @tags input
    WHERE NOT EXISTS (SELECT TOP 1 1 FROM cati.tags t WHERE input.tag = t.tag)

    MERGE cati.posttags pt
    using @tagIds src
       ON src.id = pt.tag AND pt.post = @id
    WHEN NOT MATCHED THEN
        INSERT
        (
            post
           ,tag
        )
        VALUES
        (
            @id
           ,src.id
        )
    WHEN NOT MATCHED BY SOURCE AND pt.post = @id
      THEN DELETE;

    COMMIT TRANSACTION

    EXECUTE @error = cati.getposts @error_message = @error_message
                                  ,@id = @id
                                  ,@includeUnpublished = 1
                                  ,@includeDeleted = 1

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

