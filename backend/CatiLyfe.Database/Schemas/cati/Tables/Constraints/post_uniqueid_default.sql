ALTER TABLE cati.postcontent
    ADD CONSTRAINT [post_uniqueid_default]
    DEFAULT NEWID()
    FOR uniqueid
