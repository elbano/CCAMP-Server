IF DB_ID('CCAMP') IS NULL
BEGIN
	CREATE DATABASE [CCAMP]
    PRINT 'CREATING CCAMP DATABASE'
END
ELSE
BEGIN
	PRINT 'DATABASE CCAMP ALREADY EXIST'
END

-- Make sure SQL Server has mixed configuration for authentication, via SQL Managment studio -> Windows and SQL Server Authentication Mode (Mixed Mode). Follow steps of
-- https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/change-server-authentication-mode#SSMSProcedure
-- Stop SQL Server in service and start to update the changes in the server

BEGIN
    EXEC sp_configure 'contained database authentication', 1;
    RECONFIGURE
END
 

USE [master]  
GO  
ALTER DATABASE [CCAMP] SET CONTAINMENT = PARTIAL  
GO

