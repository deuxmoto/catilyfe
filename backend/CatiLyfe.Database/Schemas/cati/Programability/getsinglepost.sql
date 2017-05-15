-- This gets a single post

CREATE PROCEDURE cati.getsinglepost
    @error_messsage NVARCHAR(2048) OUTPUT
   ,
AS
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT



RETURN 0
