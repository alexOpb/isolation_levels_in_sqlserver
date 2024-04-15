<Query Kind="SQL">
  <Connection>
    <ID>2d776bb0-e7a0-49bf-b38b-baa0e874ad3b</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>localhost,1433</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>TestDB</Database>
    <SqlSecurity>true</SqlSecurity>
    <UserName>SA</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAALn3JMDvU5UuD0f1gtRP/ygAAAAACAAAAAAAQZgAAAAEAACAAAAC4Bs5ifA567x7l7sENYaRVgceWO7tcd+HISegav1IKjgAAAAAOgAAAAAIAACAAAAAVypK65FXnBehOuC7SFuaEug1GcOWWteeCATJKayVjxRAAAADZ+okM2j+di39WiAckwIxjQAAAADFxCivOX0JIGMLvEwskj7WRBo+q2kLvr9gdyUJYEjhV2Xxe9Q4RLeU5k5dojjHdxxl7ju+a7fv87UKgIgoMyMQ=</Password>
    <DisplayName>Connection 2</DisplayName>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

-- 1. T1
-- 2. T2
--SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
--SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;
--SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRANSACTION T2
UPDATE users SET name = 'User1**' WHERE id = 1;
SELECT * FROM users; 
COMMIT TRANSACTION T2;
-- 3. T1
-- 4. T1
