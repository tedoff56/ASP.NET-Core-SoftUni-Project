using LightBulbsStore.Core.Services.Contracts;

namespace LightBulbsStore.Services;

public class TextShortenerService : ITextShortenerService
{
    public string Transform(string text, int length) 
    {
        var result = text;

        if(length < text.Length)
        {
            result = text.Substring(0, length) + "...";
        }

        return result;
    }
}