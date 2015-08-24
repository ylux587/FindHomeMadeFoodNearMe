CREATE TYPE [dbo].[typ_Users_v1] AS TABLE
(
    [Email] NVARCHAR(200) NOT NULL, 
    [Password] NVARCHAR(20) NOT NULL,
    [FirstName] NVARCHAR(200) NOT NULL, 
    [LastName] NVARCHAR(200) NOT NULL, 
    [PhoneNumber] NVARCHAR(200) NOT NULL,
    [UserStatus] INT NOT NULL 
)
