namespace FoodTracker.Application.Features.Nutrition;

public static class FoodErrorCodes
{
    public const string FoodItemNotFound = "food.item_not_found";
    public const string FoodEntryNotFound = "food.entry_not_found";
    public const string FoodItemForbidden = "food.item_forbidden";
    public const string EntryNotFound = "food.entry_not_found";
    public const string InvalidAmount = "food.invalid_amount";
    public const string InvalidPatch = "food.invalid_patch";
    public const string InvalidCategory = "food.invalid_category";
    public const string MissingPortionSize = "food.missing_portion_size";
    public const string MissingConsumptionAmount = "food.missing_consumption_amount";
}