CREATE PROCEDURE [dbo].[SaveUsers]
    @users typ_Users_v1 READONLY
AS
BEGIN
    SET NOCOUNT ON

    MERGE INTO [dbo].[Users] AS TARGET
    USING
    (
        SELECT
            [Email]
            [Password],
            [FirstName], 
            [LastName], 
            [PhoneNumber],
            [UserStatus]
        FROM @users
    ) AS SOURCE ON (TARGET.[Email] = SOURCE.[Email] OR TARGET.[PhoneNumber] = SOURCE.[PhoneNumber])
    WHEN MATCHED THEN
        UPDATE SET
            TARGET.[Email] = SOURCE.[Email], 
            TARGET.[Password] = SOURCE.[Password],
            TARGET.[FirstName] = SOURCE.[FirstName], 
            TARGET.[LastName] = SOURCE.[LastName], 
            TARGET.[PhoneNumber] = SOURCE.[PhoneNumber],
            TARGET.[UserStatus] = SOURCE.[UserStatus]
    WHEN NOT MATCHED THEN
        INSERT
        (
            [Email],
            [Password],
            [FirstName],
            [LastName], 
            [PhoneNumber],
            [UserStatus]
        )
        VALUES
        (
            SOURCE.[Email], 
            SOURCE.[Password],
            SOURCE.[FirstName], 
            SOURCE.[LastName], 
            SOURCE.[PhoneNumber], 
            SOURCE.[UserStatus]
        );

END