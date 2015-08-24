CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
    SET NOCOUNT ON

    SELECT
        [UserId], 
        [Password],
        [FirstName], 
        [LastName], 
        [Email], 
        [PhoneNumber],
        [UserStatus]
    FROM [dbo].[Users]
END
