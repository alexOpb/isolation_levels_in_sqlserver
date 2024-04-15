# Setup

```docker
docker pull mcr.microsoft.com/mssql/server:2019-latest
```

```docker
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=qwerty123' -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server:2019-latest
```

1. In DOcker desktop open an external terminal
2. /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P qwerty123

# LinqPad

1. Add new connection
2. Linq to SQL
3. Server: localhost,1433
4. SQL Authentication
5. User name: SA
6. Password: qwerty123
7. Test
8. Ok
9. Repeat 1-8 to create the second connection.
10. Open the folder in LinkPad.
11. Open T1. Select Connection 1.
12. Open T2 in another LInkPad instance for conviniece. Select Connection 2.
13. Run T1 and T2 in the order listed in the files.
14. Run Restart portion in T1 every time you want to restart the table.

# Findings

I will compare my findings with PosgreSQL based on the previous test: https://github.com/alexOpb/isolation_levels_in_postgresql

1. Test 1 Dirty Read
   - T1's isolation doesn't matter. T2 which attempts to read uncommitted data matters.
   - T2 can read the T1's uncommitted update if it only has: "READ UNCOMMITTED" isolation level.
2. Test 2 Lost Update
   - Sql Server: All isolation levels do not solve the 'Lost Update' in mssql.
   - PostgreSQL: Repeatable Read and Serializable solve the problem.
3. Test 3 Repeatable Read
   - Sql Server: Repeatable Read and Serializable solve Repeatable read problem by not allowing to T2 commit until T1 commits. But after T1 commits T2 automatically commits.
   - PostgreSQL: same but after T1 commits T2 can't commit its changes, T2 will rollback after the commit.
4. Test 4 Phantom Read
   - SQL Server:
     - repeatable read: Yes. Phantom read happens.
     - serializable: No. Phantom read is solved.
   - PostreSQL:
     - repeatable read: No. Phantom read is solved.
     - serializable: same as repeatable read
   - DIFF:
     - 1\. SQL Server "Repeatable Read" doesn't solve Phantom read. In PostgreSQL it solve it.
     - 2\. SQL Server: Serializable: T2 waits T1. When T1 commits, T2 commits its insertion. But in PosgreSQL: Repeatable Read / Serializable: T2 doesn't wait for T1 to commit. They commit. But T1 can't see the insertion until it commits.
5. What does T1 block (a cell, a row, or table)?
   - SQL Server: Row is blocked. We can change another row.
   - PostgreSQL: Row is blocked. We can change another row.

## SQL Server vs. Posgresql:

|                  | Lost Update                    | Dirty Read                     | Repeatable Read                | Phantom Read                   |
| ---------------- | ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| Read Uncommitted | SQL Server: ❌, PostgreSQL: ❌ | SQL Server: ❌, PostgreSQL: ✅ | SQL Server: ❌, PostgreSQL: ❌ | SQL Server: ❌, PostgreSQL: ❌ |
| Read Committed   | SQL Server: ❌, PostgreSQL: ❌ | SQL Server: ✅, PostgreSQL: ✅ | SQL Server: ❌, PostgreSQL: ❌ | SQL Server: ❌, PostgreSQL: ❌ |
| Repeatable Read  | SQL Server: ❌, PostgreSQL: ✅ | SQL Server: ✅, PostgreSQL: ✅ | SQL Server: ✅, PostgreSQL: ✅ | SQL Server: ❌, PostgreSQL: ✅ |
| Serializable     | SQL Server: ❌, PostgreSQL: ✅ | SQL Server: ✅, PostgreSQL: ✅ | SQL Server: ✅, PostgreSQL: ✅ | SQL Server: ✅, PostgreSQL: ✅ |
