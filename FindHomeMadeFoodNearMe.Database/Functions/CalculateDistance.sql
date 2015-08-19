CREATE FUNCTION [dbo].[CalculateDistance]
(
    @sourceLat float,
    @sourceLong float,
    @targetLat float,
    @targetLong float
)
RETURNS float
AS
BEGIN
    DECLARE @distance float

    SET @distance = 3959 * ACOS(COS(RADIANS(@sourceLat))*COS(RADIANS(@targetLat))*COS(RADIANS(@targetLong)-RADIANS(@sourceLong))+SIN(RADIANS(@sourceLat))*SIN(RADIANS(@targetLat)));

    RETURN @distance;
END
