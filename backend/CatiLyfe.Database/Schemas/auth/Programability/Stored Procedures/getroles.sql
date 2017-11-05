CREATE PROCEDURE auth.getroles
    @error_message  NVARCHAR(2048) OUTPUT
AS
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT

    SELECT
        r.role
       ,r.description
    FROM auth.roles r

RETURN 0
