-- This gets a single post

CREATE PROCEDURE cati.getposts
    @error_message NVARCHAR(2048) OUTPUT
   ,@top        INT = NULL
   ,@skip       INT = NULL
   ,@startdate  DATETIME2 = NULL
   ,@enddate    DATETIME2 = NULL
AS
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT

    DECLARE @error INT = 0
    SET @top =          ISNULL(@top, 10)
    SET @skip =         ISNULL(@skip, 0)
    SET @startdate =    ISNULL(@startdate, '1900-1-1')
    SET @enddate =      ISNULL(@enddate, '2400-12-20') -- TODO: Fix this in the year 2400

    DECLARE @selected TABLE
    (
        id          INT             NOT NULL
       ,slug        NVARCHAR(256)   NOT NULL
       ,title       NVARCHAR(256)   NOT NULL
       ,goeslive    DATETIME2       NOT NULL
       ,created     DATETIME2       NOT NULL
       ,description NVARCHAR(256)   NOT NULL
    )

    INSERT INTO @selected
    SELECT
        m.id
       ,m.slug
       ,m.title
       ,m.goeslive
       ,m.created
       ,m.description
    FROM cati.postmeta m
    WHERE m.goeslive BETWEEN @startdate AND @enddate
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

ErrorHandler:
    RETURN @error
