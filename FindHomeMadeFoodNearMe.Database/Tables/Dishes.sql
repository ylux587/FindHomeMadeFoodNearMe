CREATE TABLE [dbo].[Dishes]
(
    [DishId] BIGINT NOT NULL IDENTITY(1,1), 
    [ProviderId] BIGINT NOT NULL, 
    [DishType] INT NOT NULL, 
    [DishName] NVARCHAR(200) NOT NULL, 
    [Description] NVARCHAR(500) NOT NULL, 
    [Ingredients] NVARCHAR(200) NOT NULL, 
    [Price] DECIMAL(4, 2) NOT NULL, 
    [WaitingTimeInMins] INT NOT NULL, 
    [ThumbNailPictureKey] UNIQUEIDENTIFIER NULL, 
    [Available] BIT NOT NULL, 
    CONSTRAINT [PK_Dishes] PRIMARY KEY ([DishId])
)

GO
