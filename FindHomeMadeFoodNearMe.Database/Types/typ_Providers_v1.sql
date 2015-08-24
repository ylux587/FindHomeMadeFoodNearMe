CREATE TYPE [dbo].[typ_Providers_v1] AS TABLE
(
    [ProviderId] BIGINT NOT NULL, 
    [AddressLine1] NVARCHAR(200) NOT NULL, 
    [AddressLine2] NVARCHAR(200) NULL, 
    [AddressLine3] NVARCHAR(200) NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [StateOrProvince] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(10) NOT NULL,
    [ZipCode] NVARCHAR(10) NULL, 
    [ProviderStatus] INT NOT NULL, 
    [GeoLatitude] FLOAT NOT NULL, 
    [GeoLongitude] FLOAT NOT NULL
)
