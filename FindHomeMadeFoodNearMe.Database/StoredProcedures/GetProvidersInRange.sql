CREATE PROCEDURE [dbo].[GetProvidersInRange]
    @sourceLatitude float, 
    @sourceLongitude float,
    @range INT,
    @convertToMile BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT 
        p.[ProviderId],
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
    INNER JOIN Dishes d ON d.[ProviderId] = p.ProviderId
    WHERE u.[UserStatus] = 1 AND p.[ProviderStatus] = 1 AND p.GeoLatitude IS NOT NULL AND p.GeoLongitude IS NOT NULL
    AND [dbo].[CalculateDistance](@sourceLatitude, @sourceLongitude, p.GeoLatitude, p.GeoLongitude, @convertToMile) <= @range
    AND d.[Available] = 1

END
