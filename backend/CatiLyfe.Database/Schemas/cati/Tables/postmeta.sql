﻿CREATE TABLE cati.postmeta
(
    id          INT             NOT NULL
   ,slug        NVARCHAR(256)   NOT NULL
   ,title       NVARCHAR(256)   NOT NULL
   ,goeslive    DATETIME2       NOT NULL
   ,created     DATETIME2       NOT NULL
   ,description NVARCHAR(256)   NOT NULL
)
