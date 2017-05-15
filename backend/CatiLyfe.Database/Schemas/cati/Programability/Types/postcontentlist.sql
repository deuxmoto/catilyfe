CREATE TYPE cati.postcontentlist AS TABLE
(
    id      INT NOT NULL
   ,type    NVARCHAR(32)
   ,content NVARCHAR(MAX)
)
