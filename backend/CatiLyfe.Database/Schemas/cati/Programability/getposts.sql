-- This gets a single post

CREATE PROCEDURE cati.getposts
    @error_message NVARCHAR(2048) OUTPUT
   ,@top                   INT = NULL
   ,@skip                  INT = NULL
   ,@startdate             DATETIME2 = NULL
   ,@enddate               DATETIME2 = NULL
   ,@includeUnpublished    BIT = 0
   ,@includeDeleted        BIT = 0
   ,@id                    INT = NULL
   ,@slug                  NVARCHAR(256) = NULL
   ,@tags                  cati.tagslist READONLY
AS
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT

    DECLARE @error INT = 0
    SET @top =          ISNULL(@top, 10)
    SET @skip =         ISNULL(@skip, 0)
    SET @startdate =    ISNULL(@startdate, '1900-1-1')
    SET @enddate =      ISNULL(@enddate, '2400-12-20') -- TODO: Fix this in the year 2400
    DECLARE @emptyTags  BIT = 0

    DECLARE @tagids TABLE
    (
        id INT PRIMARY KEY
    )

    IF(EXISTS (SELECT TOP 1 1 FROM @tags))
    BEGIN
        INSERT @tagids
        SELECT
            t.id
        FROM @tags tn
        JOIN cati.tags t
          ON t.tag = tn.tag
    END
    ELSE
    BEGIN
        SET @emptyTags = 1
    END

    DECLARE @selected TABLE
    (
        id          INT             NOT NULL
       ,slug        NVARCHAR(256)   NOT NULL
       ,title       NVARCHAR(256)   NOT NULL
       ,goeslive    DATETIME2       NOT NULL
       ,created     DATETIME2       NOT NULL
       ,description NVARCHAR(256)   NOT NULL
       ,ispublished BIT             NOT NULL
       ,isreserved  BIT             NOT NULL
       ,isdeleted   BIT             NOT NULL
    )

    INSERT INTO @selected
    SELECT
        m.id
       ,m.slug
       ,m.title
       ,m.goeslive
       ,m.created
       ,m.description
       ,m.ispublished
       ,m.isreserved
       ,m.isdeleted
    FROM cati.postmeta m
    WHERE m.goeslive BETWEEN @startdate AND @enddate
      AND (isdeleted = 0 OR @includeDeleted = 0)
      AND (ispublished = 1 OR @includeUnpublished = 1)
      AND (EXISTS (SELECT TOP 1 1 
                  FROM cati.posttags pt
                  JOIN @tagids ids
                    ON pt.tag = ids.id
                  WHERE m.id = pt.post
                 ) OR @emptyTags = 1)
      AND (id = @id OR @id IS NULL)
      AND (slug = @slug OR @slug IS NULL)
    ORDER BY m.goeslive DESC
    OFFSET (@skip) ROWS 
    FETCH NEXT (@top) ROWS ONLY

    SELECT * FROM @selected

    SELECT
        p.id
       ,p.postid
       ,p.type
       ,p.content
    FROM cati.postcontent p
    WHERE p.postid IN (SELECT id FROM @selected)

    -- Find the tags
    SELECT
       s.id AS post
      ,t.tag
    FROM cati.posttags pt
    JOIN cati.tags t
      ON t.id = pt.tag
    JOIN @selected s
      ON s.id = pt.post

ErrorHandler:
    RETURN @error
