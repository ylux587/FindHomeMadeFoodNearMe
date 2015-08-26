namespace FindHomeMadeFoodNearMe.Services.Models.Enums
{
    public enum ProviderStatus : int
    {
        PendingVerification = 0,
        Verified = 1,
        FailedOnVerifyAddress = 2,
    }
}