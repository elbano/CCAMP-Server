USE [CCAMP]
GO
CREATE LOGIN [CCAMPAdmin] WITH PASSWORD = 'LaFxfShTYzbGph98XJCufQCxNYYPcZbU', DEFAULT_DATABASE=[CCAMP], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [CCAMPAdmin] FOR LOGIN [CCAMPAdmin]
GO
ALTER ROLE [db_owner] ADD MEMBER [CCAMPAdmin]
GO

USE [CCAMP]
GO
CREATE LOGIN [CCAMPTransactions] WITH PASSWORD = '7YVUaK3AAGvF8RwUJKwgavGAdLkavcDx', DEFAULT_DATABASE=[CCAMP], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [CCAMPTransactions] FOR LOGIN [CCAMPTransactions]
GO
GRANT INSERT, SELECT, UPDATE, DELETE ON DATABASE :: CCAMP TO CCAMPTransactions
GO
DENY ALTER ON DATABASE :: CCAMP TO CCAMPTransactions
GO
