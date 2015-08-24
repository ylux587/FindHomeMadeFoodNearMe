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
        u.[UserStatus],
        p.[AddressLine1], 
        p.[AddressLine2], 
        p.[AddressLine3], 
        p.[City], 
        p.[StateOrProvince], 
        p.[Country],
        p.[ZipCode], 
        p.[ProviderStatus], 
        p.[GeoLatitude], 
        p.[GeoLongitude]
    FROM Users u
    INNER JOIN Providers p ON u.UserId = p.ProviderId
    INNER JOIN Dishes d ON p.ProviderId = d.ProviderId
    WHERE u.[UserStatus] = 1 AND p.[ProviderStatus] = 1 AND p.GeoLatitude <> 0 AND p.GeoLongitude <> 0
    AND [dbo].[CalculateDistance](@sourceLatitude, @sourceLongtitude, p.GeoLatitude, p.GeoLongitude) <= @range

END
