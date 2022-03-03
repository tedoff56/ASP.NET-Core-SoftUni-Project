namespace LightBulbsStore.Services.Contracts;

public interface ITextShortenerService
{
    string Transform(string text, int length);
}