namespace LightBulbsStore.Core.Services.Contracts;

public interface ITextShortenerService
{
    string Transform(string text, int length);
}