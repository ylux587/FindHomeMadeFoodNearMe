CREATE FUNCTION [dbo].[CalculateDistance]
(
    @sourceLat float,
    @sourceLong float,
    @targetLat float,
    @targetLong float,
    @convertToMile BIT = 1
)
RETURNS float
AS
BEGIN
    DECLARE @distance float

    -- Convert to radians
    SET @sourceLat = @sourceLat / 57.2958
    SET @sourceLong = @sourceLong / 57.2958
    SET @targetLat = @targetLat / 57.2958
    SET @targetLong = @targetLong / 57.2958
    -- Calc distance
    SET @distance = (SIN(@sourceLat) * SIN(@targetLat)) + (COS(@sourceLat) * COS(@targetLat) * COS(@sourceLong - @targetLong))
    -- Convert to miles
    IF (@distance <> 0 AND @convertToMile = 1)
    BEGIN
        SET @distance = 3958.75 * ATAN(SQRT(1 - POWER(@distance, 2)) / @distance);
    END

    RETURN @distance;
END
