CREATE PROCEDURE [dbo].[SaveUsers]
    @users typ_Users_v1 READONLY
AS
BEGIN
    SET NOCOUNT ON

    MERGE INTO [dbo].[Users] AS TARGET
    USING
    (
        SELECT
            [UserId], 
            [Password],
            [FirstName], 
            [LastName], 
            [Email], 
            [PhoneNumber], 
            [AddressLine1], 
            [AddressLine2], 
            [AddressLine3], 
            [City], 
            [StateOrProvince], 
            [Country],
            [ZipCode], 
            [Status], 
            [GeoLatitude], 
            [GeoLongitude]
        FROM @users
    ) AS SOURCE ON (TARGET.[UserId] = SOURCE.[UserId] OR TARGET.[Email] = SOURCE.[Email])
    WHEN MATCHED THEN
        UPDATE SET
            TARGET.[Password] = SOURCE.[Password],
            TARGET.[FirstName] = SOURCE.[FirstName], 
            TARGET.[LastName] = SOURCE.[LastName], 
            TARGET.[Email] = SOURCE.[Email], 
            TARGET.[PhoneNumber] = SOURCE.[PhoneNumber], 
            TARGET.[AddressLine1] = SOURCE.[AddressLine1], 
            TARGET.[AddressLine2] = SOURCE.[AddressLine2], 
            TARGET.[AddressLine3] = SOURCE.[AddressLine3], 
            TARGET.[City] = SOURCE.[City], 
            TARGET.[StateOrProvince] = SOURCE.[StateOrProvince], 
            TARGET.[Country] = SOURCE.[Country],
            TARGET.[ZipCode] = SOURCE.[ZipCode], 
            TARGET.[Status] = SOURCE.[Status], 
            TARGET.[GeoLatitude] = SOURCE.[GeoLatitude], 
            TARGET.[GeoLongitude] = SOURCE.[GeoLongitude]
    WHEN NOT MATCHED AND NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE [Email] = SOURCE.[Email]) THEN
        INSERT
        (
            [Password],
            [FirstName], 
            [LastName], 
            [Email], 
            [PhoneNumber], 
            [AddressLine1], 
            [AddressLine2], 
            [AddressLine3], 
            [City], 
            [StateOrProvince], 
            [Country],
            [ZipCode], 
            [Status], 
            [GeoLatitude], 
            [GeoLongitude] 
        )
        VALUES
        (
            SOURCE.[Password],
            SOURCE.[FirstName], 
            SOURCE.[LastName], 
            SOURCE.[Email], 
            SOURCE.[PhoneNumber], 
            SOURCE.[AddressLine1], 
            SOURCE.[AddressLine2], 
            SOURCE.[AddressLine3], 
            SOURCE.[City], 
            SOURCE.[StateOrProvince], 
            SOURCE.[Country],
            SOURCE.[ZipCode], 
            SOURCE.[Status], 
            SOURCE.[GeoLatitude], 
            SOURCE.[GeoLongitude]
        );

END