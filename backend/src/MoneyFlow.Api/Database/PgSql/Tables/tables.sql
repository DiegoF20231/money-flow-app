CREATE TABLE users
(
    user_id  INT                 NOT NULL PRIMARY KEY,
    username VARCHAR(255)        NOT NULL,
    email    VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255)        NOT NULL
);

-- CREATE INDEX idx_users_email ON users (email);

CREATE TABLE categories
(
    category_id INT                 NOT NULL PRIMARY KEY,
    name        TEXT UNIQUE NOT NULL,
    color       VARCHAR(255)        NOT NULL,
    user_id     INT                 NOT NULL

    --CONSTRAINT fk_post_users
        --FOREIGN KEY (user_id)
            --REFERENCES users (user_id)
            --ON DELETE RESTRICT
);
