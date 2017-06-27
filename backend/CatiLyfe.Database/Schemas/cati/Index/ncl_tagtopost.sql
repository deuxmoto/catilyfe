-- NCL map from tags back to posts

CREATE INDEX ncl_tagtopost
    ON cati.posttags
    (tag)