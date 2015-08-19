namespace FineHomeMadeFoodNearMe.Services.Models.Enums
{
    public enum UserStatus : int
    {
        PendingVerification = 0,
        Verified = 1,
        FailedOnVerifyAddress = 2,
    }
}