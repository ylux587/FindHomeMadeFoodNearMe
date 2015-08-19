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
    FROM [dbo].[Users]
END
