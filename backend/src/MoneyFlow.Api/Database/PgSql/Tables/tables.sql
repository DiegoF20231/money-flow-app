CREATE TABLE users
(
    user_id  UUID                NOT NULL PRIMARY KEY,
    username VARCHAR(255)        NOT NULL,
    email    VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255)        NOT NULL
);

CREATE TABLE categories
(
    category_id UUID         NOT NULL PRIMARY KEY,
    name        VARCHAR(255) NOT NULL,
    color       VARCHAR(7)   NOT NULL,
    type        VARCHAR(255) NOT NULL CHECK ( type IN ('INCOME', 'EXPENSE') ),
    user_id     UUID         NOT NULL,

    CONSTRAINT fk_categories_users
        FOREIGN KEY (user_id)
            REFERENCES users (user_id)
            ON DELETE RESTRICT,

    CONSTRAINT uq_categories_user_name
        UNIQUE (category_id, name)
);

CREATE TABLE transactions
(

    transaction_id UUID         NOT NULL PRIMARY KEY,
    description    VARCHAR(255) NOT NULL,
    amount         DECIMAL      NOT NULL,
    type           VARCHAR(255) NOT NULL CHECK (type IN ('INCOME', 'EXPENSE')),
    user_id        UUID         NOT NULL,
    category_id    UUID         NOT NULL,
    date           DATE,

    CONSTRAINT fk_transactions_users
        FOREIGN KEY (user_id)
            REFERENCES users (user_id)
            ON DELETE RESTRICT,

    CONSTRAINT fk_transactions_categories
        FOREIGN KEY (category_id)
            REFERENCES categories (category_id)
            ON DELETE RESTRICT


);