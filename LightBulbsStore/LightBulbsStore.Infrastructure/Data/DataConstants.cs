namespace LightBulbsStore.Infrastructure.Data;

public class DataConstants
{
    public const int GuidIdMaxLength = 64;
    public const int UrlMaxLength = 2048;
    
    //Customer constants
    public const int CustomerEmailAddressMaxLength = 320;
    public const int CustomerNameMaxLength = 64;
    public const int CustomerPhoneNumberMaxLength = 16;
    public const int CustomerCityNameMaxLength = 30;
    public const int CustomerStreetAddressMaxLength = 95;

    //Product constants
    public const int ProductNameMinLength = 5;
    public const int ProductNameMaxLength = 150;
    public const int ProductDescriptionMinLength = 20;
    public const int ProductDescriptionMaxLength = 2000;

    //Category constants
    public const int CategoryNameMaxLength = 30;

}