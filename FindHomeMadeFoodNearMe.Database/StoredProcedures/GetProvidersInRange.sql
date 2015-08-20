CREATE PROCEDURE [dbo].[GetProvidersInRange]
    @sourceLatitude float, 
    @sourceLongtitude float,
    @range INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT 
        u.[UserId],
        u.[Password],
        u.[FirstName], 
        u.[LastName], 
        u.[Email], 
        u.[PhoneNumber], 
        u.[AddressLine1], 
        u.[AddressLine2], 
        u.[AddressLine3], 
        u.[City], 
        u.[StateOrProvince], 
        u.[Country],
        u.[ZipCode], 
        u.[Status], 
        u.[GeoLatitude], 
        u.[GeoLongitude]
    FROM Users u
    INNER JOIN Dishes d ON u.UserId = d.ProviderId
    WHERE u.[Status] = 1 AND u.GeoLatitude IS NOT NULL AND u.GeoLongitude IS NOT NULL
    AND [dbo].[CalculateDistance](@sourceLatitude, @sourceLongtitude, u.GeoLatitude, u.GeoLongitude) <= @range

END
