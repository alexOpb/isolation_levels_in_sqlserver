<Query Kind="SQL">
  <Connection>
    <ID>30ee8d3b-a1c9-4ca0-8826-8c308231ea99</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>localhost,1433</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <SqlSecurity>true</SqlSecurity>
    <UserName>SA</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAALn3JMDvU5UuD0f1gtRP/ygAAAAACAAAAAAAQZgAAAAEAACAAAADNAGP0PIHv/WkoRdTpsHTb078UReSNjzXE7f1XonUgAgAAAAAOgAAAAAIAACAAAAAjgAnmXtXobMovCH0LrqjfJyepd4h7n+Gad1vMuugmeBAAAADkNtv109o3y5iOm6qr55dRQAAAAIDXK+p1pLswsHmp/fItoEGQdyw5Q4Zl2tfQZBMHEsIGPRA2O5QXsPoxJvW9rO4CHlq4d6KZ1MymH8oOE8C06m0=</Password>
    <Database>TestDB</Database>
    <DisplayName>Connection 1</DisplayName>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

-- 1. T1
SELECT * FROM users;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
--SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
--SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;
--SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRANSACTION T1
UPDATE users SET name = 'User1*' WHERE id = 1;
SELECT * FROM users;

-- 2. T2
-- 4. T1
COMMIT TRANSACTION T1;
-- 5. T2


-- restart
USE TestDB;
IF OBJECT_ID('users', 'U') IS NOT NULL DROP TABLE users;
CREATE TABLE users (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(50),
  email VARCHAR(50)
);
INSERT INTO users (name, email) 
VALUES ('User1', 'user1@example.com'),
       ('User2', 'user2@example.com'),
('User3', 'user3@example.com');
SELECT * FROM users;