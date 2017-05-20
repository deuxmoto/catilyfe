-- the primary key on post content

ALTER TABLE cati.postcontent
    ADD CONSTRAINT [pk_postcontent]
    PRIMARY KEY (postid, id)
