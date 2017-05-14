-- the primary key on post metadata

ALTER TABLE cati.postmeta
    ADD CONSTRAINT [pk_postmeta]
    PRIMARY KEY (id)
