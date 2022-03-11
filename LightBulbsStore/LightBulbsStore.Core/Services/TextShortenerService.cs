using LightBulbsStore.Core.Services.Contracts;

namespace LightBulbsStore.Services;

public class TextShortenerService : ITextShortenerService
{
    public string Transform(string text, int length)
        => text.Substring(0, length) + "...";
}