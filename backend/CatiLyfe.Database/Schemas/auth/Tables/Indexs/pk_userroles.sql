ALTER TABLE auth.userroles
    ADD CONSTRAINT [pk_userroles]
    PRIMARY KEY (userid, roleid)
