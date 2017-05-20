-- Gets post metadata

CREATE PROCEDURE cati.getpostmetadata
    @error_message NVARCHAR(2048) OUTPUT
   ,@top            INT = NULL
   ,@skip           INT = NULL
   ,@startdate      DATETIME2 = NULL
   ,@enddate        DATETIME2 = NULL
AS
    SET NOCOUNT ON
    -- Run as snapshot
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT

    SET @top =          ISNULL(@top, 100)
    SET @skip =         ISNULL(@skip, 0)
    SET @startdate =    ISNULL(@startdate, '1900-1-1')
    SET @enddate =      ISNULL(@enddate, '2400-12-20') -- TODO: Fix this in the year 2400

    SELECT
        p.id
       ,p.slug
       ,p.title
       ,p.goeslive
       ,p.description
       ,p.created
    FROM cati.postmeta p
    WHERE p.goeslive BETWEEN @startdate AND @enddate
    ORDER BY p.goeslive DESC
    OFFSET (@skip) ROWS 
    FETCH NEXT (@top) ROWS ONLY

RETURN 0
