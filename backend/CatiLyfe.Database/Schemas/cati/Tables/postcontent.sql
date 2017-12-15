CREATE TABLE cati.postcontent
(
    postid      INT              NOT NULL
   ,id          INT              NOT NULL
   ,type        NVARCHAR(32)     NOT NULL
   ,content     NVARCHAR(MAX)    NOT NULL
   ,uniqueid    UNIQUEIDENTIFIER NOT NULL
)
