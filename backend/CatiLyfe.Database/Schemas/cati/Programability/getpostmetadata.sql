-- Gets post metadata

CREATE PROCEDURE cati.getpostmetadata
    @error_message NVARCHAR(2048) OUTPUT
   ,@top            INT = NULL
   ,@skip           INT = NULL
   ,@startdate      DATETIME2 = NULL
   ,@enddate        DATETIME2 = NULL
   ,@tags           cati.tagslist READONLY
AS
    SET NOCOUNT ON
    -- Run as snapshot
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT

    SET @top =          ISNULL(@top, 100)
    SET @skip =         ISNULL(@skip, 0)
    SET @startdate =    ISNULL(@startdate, '1900-1-1')
    SET @enddate =      ISNULL(@enddate, '2400-12-20') -- TODO: Fix this in the year 2400

    DECLARE @selectedIds TABLE
    (
        id INT NOT NULL
    )

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
        INSERT @tagids
        SELECT
           id
        FROM cati.tags
    END

    INSERT INTO @selectedIds
    (
        id
    )
    SELECT
        p.id
    FROM cati.postmeta p
    WHERE p.goeslive BETWEEN @startdate AND @enddate
      AND EXISTS (SELECT TOP 1 1 
                  FROM cati.posttags pt
                  JOIN @tagids id
                    ON pt.tag = id.id
                  WHERE p.id = pt.post
                 )
    ORDER BY p.goeslive DESC
    OFFSET (@skip) ROWS 
    FETCH NEXT (@top) ROWS ONLY

    SELECT
        p.id
       ,p.slug
       ,p.title
       ,p.goeslive
       ,p.description
       ,p.created
    FROM cati.postmeta p
    JOIN @selectedIds id
      ON id.id = p.id

    -- Find the tags
    SELECT
       s.id AS post
      ,t.tag
    FROM cati.posttags pt
    JOIN cati.tags t
      ON t.id = pt.tag
    JOIN @selectedIds s
      ON s.id = pt.post

RETURN 0
