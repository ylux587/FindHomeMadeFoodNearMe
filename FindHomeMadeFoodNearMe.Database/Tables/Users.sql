CREATE TABLE [dbo].[Users]
(
    [UserId] BIGINT NOT NULL IDENTITY, 
    [Password] NVARCHAR(20) NOT NULL,
    [FirstName] NVARCHAR(200) NOT NULL, 
    [LastName] NVARCHAR(200) NOT NULL, 
    [Email] NVARCHAR(200) NOT NULL, 
    [PhoneNumber] NVARCHAR(200) NULL, 
    [AddressLine1] NVARCHAR(200) NOT NULL, 
    [AddressLine2] NVARCHAR(200) NULL, 
    [AddressLine3] NVARCHAR(200) NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [StateOrProvince] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(10) NOT NULL,
    [ZipCode] NVARCHAR(10) NULL, 
    [Status] INT NOT NULL, 
    [GeoLatitude] FLOAT NOT NULL, 
    [GeoLongitude] FLOAT NOT NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]) 
)
