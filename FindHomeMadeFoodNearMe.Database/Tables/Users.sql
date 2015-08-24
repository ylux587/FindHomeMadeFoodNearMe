CREATE TABLE [dbo].[Users]
(
    [UserId] BIGINT NOT NULL , 
    [Email] NVARCHAR(200) NOT NULL, 
    [Password] NVARCHAR(20) NOT NULL,
    [FirstName] NVARCHAR(200) NOT NULL,
    [LastName] NVARCHAR(200) NOT NULL, 
    [PhoneNumber] NVARCHAR(200) NOT NULL, 
    [UserStatus] INT NOT NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]) 
)
