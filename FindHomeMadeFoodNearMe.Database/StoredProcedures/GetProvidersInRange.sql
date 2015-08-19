CREATE PROCEDURE [dbo].[GetProvidersInRange]
    @sourceLatitude float, 
    @sourceLongtitude float,
    @range INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT u.UserId
    FROM Users u
    INNER JOIN Dishes d ON u.UserId = d.ProviderId
    WHERE u.[Status] = 1 AND u.GeoLatitude IS NOT NULL AND u.GeoLongitude IS NOT NULL
    AND [dbo].[CalculateDistance](@sourceLatitude, @sourceLongtitude, u.GeoLatitude, u.GeoLongitude) <= @range

END
