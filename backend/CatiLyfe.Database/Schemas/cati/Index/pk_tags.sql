-- the primary key on tags

ALTER TABLE cati.tags
    ADD CONSTRAINT [pk_tags]
    PRIMARY KEY (id)