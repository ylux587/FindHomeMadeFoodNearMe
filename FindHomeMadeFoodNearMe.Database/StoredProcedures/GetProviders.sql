CREATE PROCEDURE [dbo].[GetProviders]
AS
BEGIN
    SET NOCOUNT ON

    SELECT
        [ProviderId], 
        [AddressLine1], 
        [AddressLine2], 
        [AddressLine3], 
        [City], 
        [StateOrProvince], 
        [Country],
        [ZipCode], 
        [ProviderStatus], 
        [GeoLatitude], 
        [GeoLongitude]
    FROM [dbo].[Providers]
END
