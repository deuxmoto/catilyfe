ALTER TABLE auth.roles
    ADD CONSTRAINT [default_role_deleted]
    DEFAULT 0
    FOR isdeleted
