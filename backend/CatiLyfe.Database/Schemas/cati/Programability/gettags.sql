CREATE PROCEDURE cati.gettags
    @error_message NVARCHAR(2048) OUTPUT
AS
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT
    SET NOCOUNT ON

    SELECT
       t.tag
      ,COUNT(*) AS posts
    FROM cati.tags t
    JOIN cati.posttags pt
      ON pt.tag = t.id
    JOIN cati.postmeta pm
      ON pt.post = pm.id
    WHERE pm.goeslive < GETUTCDATE()
    GROUP BY t.tag

RETURN 0
