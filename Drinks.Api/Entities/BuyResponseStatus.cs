namespace Drinks.Api.Entities
{
    public enum BuyResponseStatus
    {
        Valid,
        InsufficientFunds,
        InvalidBadge,
        InvalidHash,
        InvalidProduct,
        InvalidTimestamp,
        DeserializationException
    }
}