using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using SpecOverflow.Web.Models;
using TechTalk.SpecFlow;

namespace SpecOverflow.Specs.Support
{
    [Binding]
    public class DatabaseSupport
    {
        [BeforeScenario]
        public void RecreateDatabase()
        {
            RecreateDatabase(new SpecOverflowEntities());
        }

        public void RecreateDatabase(DbContext dbContext)
        {
            dbContext.Database.CreateIfNotExists();
            dbContext.Database.ExecuteSqlCommand(dropAllTablesSql);
            var createSchemaScript = ((IObjectContextAdapter)dbContext).ObjectContext.CreateDatabaseScript();
            dbContext.Database.ExecuteSqlCommand(createSchemaScript);
        }

        private const string dropAllTablesSql =
@"DECLARE @Sql NVARCHAR(500) DECLARE @Cursor CURSOR 
SET @Cursor = CURSOR FAST_FORWARD FOR 
SELECT DISTINCT sql = 'ALTER TABLE [' + tc2.TABLE_NAME + '] DROP [' + rc1.CONSTRAINT_NAME + ']'
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc1
LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc2 ON tc2.CONSTRAINT_NAME =rc1.CONSTRAINT_NAME
OPEN @Cursor FETCH NEXT FROM @Cursor INTO @Sql 
WHILE (@@FETCH_STATUS = 0) 
BEGIN 
Exec SP_EXECUTESQL @Sql
FETCH NEXT FROM @Cursor INTO @Sql 
END
CLOSE @Cursor DEALLOCATE @Cursor;
EXEC sp_MSForEachTable 'DROP TABLE ?'";

    }
}
