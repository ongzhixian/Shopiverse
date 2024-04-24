# Overview

## Building database project (Non-SDK-style SQL projects) from command line

Note: This assumes the sqlproj are created from within Azure Data Studio.

Copy the following 12 files from `%UserProfile%\.azuredatastudio\extensions\microsoft.sql-database-projects-1.4.1\BuildDirectory`
to local `buildDirectory`.
These files are needed to build to build a .dacpac file.

Microsoft.Data.SqlClient.dll
Microsoft.Data.Tools.Schema.Sql.dll
Microsoft.Data.Tools.Schema.SqlTasks.targets
Microsoft.Data.Tools.Schema.Tasks.Sql.dll
Microsoft.Data.Tools.Utilities.dll
Microsoft.SqlServer.Dac.dll
Microsoft.SqlServer.Dac.Extensions.dll
Microsoft.SqlServer.TransactSql.ScriptDom.dll
Microsoft.SqlServer.Types.dll
System.ComponentModel.Composition.dll
System.IO.Packaging.dll
Microsoft.SqlServer.Server.dll

ZX: Turns out documentation is wrong! The file `Microsoft.SqlServer.Server.dll` is needed too!


## Files

Files for the `buildDirectory` should be copied as follows:

C:/database-project
│   tax_rate.sql
│   warelogix.publish.xml
│   warelogix.sqlproj
│
└───buildDirectory
    │   Microsoft.Data.Tools.Schema.SqlTasks.targets
    │
    └───buildDirectory
            Microsoft.Data.SqlClient.dll
            Microsoft.Data.Tools.Schema.Sql.dll
            Microsoft.Data.Tools.Schema.Tasks.Sql.dll
            Microsoft.Data.Tools.Utilities.dll
            Microsoft.SqlServer.Dac.dll
            Microsoft.SqlServer.Dac.Extensions.dll
            Microsoft.SqlServer.Server.dll
            Microsoft.SqlServer.TransactSql.ScriptDom.dll
            Microsoft.SqlServer.Types.dll
            System.ComponentModel.Composition.dll
            System.IO.Packaging.dll
 

ZX: Yeah, I know... The documentation is not correct again. And this layout smacks of something buggy...

## Command to build

```using a relative path for NETCoreTargetsPath
dotnet build .\warelogix\warelogix.sqlproj /p:NetCoreBuild=true /p:NETCoreTargetsPath="buildDirectory"
```

You can also use an absolute path.
But you need to put all the .targets file and the DLLs in the same folder (yeah, I know. Why like that...?).

``` using absolute path
dotnet build .\warelogix\warelogix.sqlproj /p:NetCoreBuild=true /p:NETCoreTargetsPath=C:\src\azureDevOps\telara\Gastar\Gastar.SqlScripts\buildDirectory
```

Note: In case future builds failed again (maybe due to missing files), we can always refer back to to original source like:

```
dotnet build .\warelogix\warelogix.sqlproj /p:NetCoreBuild=true /p:NETCoreTargetsPath=C:\Users\zhixian\.azuredatastudio\extensions\microsoft.sql-database-projects-1.4.1\BuildDirectory
```

## Publish

dotnet tool install -g microsoft.sqlpackage

sqlpackage /Action:Publish /SourceFile:".\warelogix\bin\Debug\warelogix.dacpac" /TargetConnectionString:"Server=tcp:warelogix-sqlserver.database.windows.net,1433;Initial Catalog=warelogix;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;User ID=sqlserveradmin;Password=ENTER_PASSWORD_HERE;"

# Reference

Build a database project from command line
https://learn.microsoft.com/en-us/azure-data-studio/extensions/sql-database-project-extension-build-from-command-line


Passing relative path to NETCoreTargetsPath doesn't work #11577 
https://github.com/microsoft/azuredatastudio/issues/11577


SqlPackage
https://learn.microsoft.com/en-us/sql/tools/sqlpackage/sqlpackage?view=sql-server-ver16