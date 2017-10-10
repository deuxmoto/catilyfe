CREATE TABLE auth.tokens
(
    tokenid         INT         NOT NULL IDENTITY(1,1)
   ,userid          INT         NOT NULL
   ,token           BINARY(64)  NOT NULL
   ,expiry          DATETIME2   NOT NULL
)