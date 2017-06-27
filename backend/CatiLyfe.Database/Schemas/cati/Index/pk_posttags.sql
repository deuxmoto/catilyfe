-- Primary key. Ensure order by post for lookup.

ALTER TABLE cati.posttags
    ADD CONSTRAINT [pk_posttags]
    PRIMARY KEY (post, tag)
