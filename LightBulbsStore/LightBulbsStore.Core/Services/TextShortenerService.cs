using LightBulbsStore.Core.Services.Contracts;
using System.Text;

namespace LightBulbsStore.Services;

public class TextShortenerService : ITextShortenerService
{
    public string Transform(string text, int length)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            if(i == length)
            {
                return sb.ToString().Substring(0, length) + "...";
            }
            sb.Append(text[i]);

            if (i % 34 == 0)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
}