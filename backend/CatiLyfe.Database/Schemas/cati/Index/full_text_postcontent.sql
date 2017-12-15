CREATE FULLTEXT INDEX
    ON cati.postcontent
        (content)
    KEY INDEX ncl_postcontent_uniqueid
    ON post_content_full_text_catalog
    WITH CHANGE_TRACKING AUTO
