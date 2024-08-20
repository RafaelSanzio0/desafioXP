USE tempdb

-----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE FINANCIAL_PRODUCTS_TYPE (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NAME VARCHAR(100) NOT NULL,
    CREATED_AT DATETIME DEFAULT GETDATE(),
    UPDATED_AT DATETIME NULL,
    CREATED_BY VARCHAR(100) NOT NULL
);
INSERT INTO FINANCIAL_PRODUCTS_TYPE (NAME, CREATED_BY)
VALUES 
('Stock', 'Sanzio-PC'),
('Bond', 'Sanzio-PC'),
('FII', 'Sanzio-PC'),
('Crypto', 'Sanzio-PC'),
('CDB', 'Sanzio-PC'),
('Other', 'Sanzio-PC');
CREATE TABLE FINANCIAL_PRODUCTS (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NAME VARCHAR(100) NOT NULL,
    TYPE INT NOT NULL,
    PRICE DECIMAL(18,2) NOT NULL,
    EXPIRATION_DATE DATETIME,
    CREATED_AT DATETIME DEFAULT GETDATE(),
    UPDATED_AT DATETIME NULL,
    CREATED_BY VARCHAR(100) NOT NULL,
    FOREIGN KEY (TYPE) REFERENCES FINANCIAL_PRODUCTS_TYPE(ID)
);
INSERT INTO FINANCIAL_PRODUCTS (NAME, TYPE, PRICE, EXPIRATION_DATE, CREATED_BY)
VALUES
('Ação ABC', 1, 100.50, '2024-08-23T23:59:59', 'Sanzio-pc'),
('Titulo público EFG', 2, 150.00, '2026-06-30T23:59:59', 'Sanzio-pc'),
('FII XYZ', 3, 200.75, '2027-03-31T23:59:59', 'Sanzio-pc'),
('Crypto JKL', 4, 250.25, '2024-08-21T23:59:59', 'Sanzio-pc'),
('CBD MNO', 5, 300.00, '2024-08-31T23:59:59', 'Sanzio-pc');
-----------------------------------------------------------------------------------------------------------------------------------


-----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE USERS_TYPE (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NAME VARCHAR(100) NOT NULL,
    CREATED_AT DATETIME DEFAULT GETDATE(),
    UPDATED_AT DATETIME NULL,
    CREATED_BY VARCHAR(100) NOT NULL
);
INSERT INTO USERS_TYPE (NAME, CREATED_BY)
VALUES 
('Client', 'Sanzio-PC'),
('Admin', 'Sanzio-PC');
CREATE TABLE USERS (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NAME VARCHAR(100) NOT NULL,
    EMAIL VARCHAR(100) NOT NULL,
    DATE_OF_BIRTH DATETIME NOT NULL,
	TYPE INT NOT NULL,
    CREATED_AT DATETIME DEFAULT GETDATE(),
    UPDATED_AT DATETIME NULL,
    CREATED_BY VARCHAR(100) NOT NULL,
	FOREIGN KEY (TYPE) REFERENCES USERS_TYPE(ID)
);
INSERT INTO Users (NAME, EMAIL, DATE_OF_BIRTH, TYPE, CREATED_BY)
VALUES 
('User1', 'rafaellima1946@hotmail.com', '1998-08-20T22:33:30', 2, 'Sanzio-pc'),
('User2', 'chadli9532@uorak.com', '2001-04-20T22:33:30', 1, 'Sanzio-pc'),
('User3', 'arcelia918@uorak.com', '2001-04-20T22:33:30', 1, 'Sanzio-pc'),
('User4', 'malka7001@uorak.com', '2001-04-20T22:33:30', 2, 'Sanzio-pc');
-----------------------------------------------------------------------------------------------------------------------------------


-----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE TRANSACTIONS (
    ID INT PRIMARY KEY IDENTITY(1,1),
    FINANCIAL_PRODUCT_ID INT NOT NULL,
    USER_ID INT NOT NULL,
    TRANSACTION_DATE DATETIME NOT NULL,
    AMOUNT INT NOT NULL,
    TYPE INT NOT NULL,
    CREATED_AT DATETIME DEFAULT GETDATE(),
    UPDATED_AT DATETIME NULL,
    CREATED_BY VARCHAR(100) NOT NULL
    FOREIGN KEY (FINANCIAL_PRODUCT_ID) REFERENCES FINANCIAL_PRODUCTS(ID),
    FOREIGN KEY (USER_ID) REFERENCES USERS(ID)
);
-----------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE PORTFOLIOS (
    ID INT PRIMARY KEY IDENTITY(1,1),
    USER_ID INT NOT NULL,
    FINANCIAL_PRODUCT_ID INT NOT NULL,
    QUANTITY INT NOT NULL,
    AVERAGEPRICE DECIMAL(18,2) NOT NULL,
    CREATED_AT DATETIME DEFAULT GETDATE(),
    UPDATED_AT DATETIME NULL,
    CREATED_BY VARCHAR(100) NOT NULL
    FOREIGN KEY (USER_ID) REFERENCES USERS(ID),
    FOREIGN KEY (FINANCIAL_PRODUCT_ID) REFERENCES FINANCIAL_PRODUCTS(ID)
);
