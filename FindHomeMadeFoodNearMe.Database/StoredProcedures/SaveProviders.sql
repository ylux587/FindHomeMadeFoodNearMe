CREATE PROCEDURE [dbo].[SaveProviders]
    @providers typ_Providers_v1 READONLY
AS
BEGIN
    SET NOCOUNT ON

    MERGE INTO [dbo].[Providers] AS TARGET
    USING
    (
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
        FROM @providers
    ) AS SOURCE ON (TARGET.[ProviderId] = SOURCE.[ProviderId])
    WHEN MATCHED THEN
        UPDATE SET
            TARGET.[AddressLine1] = SOURCE.[AddressLine1], 
            TARGET.[AddressLine2] = SOURCE.[AddressLine2], 
            TARGET.[AddressLine3] = SOURCE.[AddressLine3], 
            TARGET.[City] = SOURCE.[City], 
            TARGET.[StateOrProvince] = SOURCE.[StateOrProvince], 
            TARGET.[Country] = SOURCE.[Country],
            TARGET.[ZipCode] = SOURCE.[ZipCode], 
            TARGET.[ProviderStatus] = SOURCE.[ProviderStatus], 
            TARGET.[GeoLatitude] = SOURCE.[GeoLatitude], 
            TARGET.[GeoLongitude] = SOURCE.[GeoLongitude]
    WHEN NOT MATCHED THEN
        INSERT
        (
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
        )
        VALUES
        (
            SOURCE.[AddressLine1], 
            SOURCE.[AddressLine2], 
            SOURCE.[AddressLine3], 
            SOURCE.[City], 
            SOURCE.[StateOrProvince], 
            SOURCE.[Country],
            SOURCE.[ZipCode], 
            SOURCE.[ProviderStatus], 
            SOURCE.[GeoLatitude], 
            SOURCE.[GeoLongitude]
        );

END