ALTER TABLE cati.postmeta
    ADD CONSTRAINT [isdeleted_default]
    DEFAULT 0
    FOR isdeleted