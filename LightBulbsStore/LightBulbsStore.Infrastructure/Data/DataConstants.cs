using System.Text.RegularExpressions;

namespace LightBulbsStore.Infrastructure.Data;

public class DataConstants
{
    public const string EmailValidationRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
    public const string RequiredFieldError = "Полето \"{0}\" е задължително";
    public const string MinMaxFieldError = "Полето \"{0}\" трябва да съдържа между {2} и {1} символа";
    public const int GuidIdMaxLength = 64;
    public const int UrlMaxLength = 2048;
    
    //Customer constants
    public const int CustomerNameMaxLength = 64;
    public const int CustomerPhoneNumberMaxLength = 16;
    public const int CustomerCityNameMaxLength = 189;
    public const int CustomerAddressMaxLength = 254;
    public const int CustomerZipCodeMaxLength = 4;

    //Product constants
    public const int ProductNameMinLength = 5;
    public const int ProductNameMaxLength = 150;
    public const int ProductDescriptionMinLength = 20;
    public const int ProductDescriptionMaxLength = 2000;

    //Category constants
    public const int CategoryNameMaxLength = 30;
    public const int CategoryDescriptionMaxLength = 100;

    //Contacts constants
    public const int ContactSubjectMinLength = 10;
    public const int ContactSubjectMaxLength = 78;

}