CREATE TABLE auth.users
(
    id      INT             NOT NULL IDENTITY(1,1)
   ,name    NVARCHAR(128)   NOT NULL
   ,email   NVARCHAR(256)   NOT NULL
   ,pass    BINARY(64)      NOT NULL
)
