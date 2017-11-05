CREATE TABLE auth.roles
(
    id          INT             NOT NULL IDENTITY (1,1)
   ,role        NVARCHAR(64)    NOT NULL
   ,description NVARCHAR(512)   NOT NULL
   ,isdeleted   BIT             NOT NULL
)
