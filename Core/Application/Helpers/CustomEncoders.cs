using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.Helpers;

static public class CustomEncoders
{
    public static string UrlEncode(this string value)
    {
        byte[] tokenBytes = Encoding.UTF8.GetBytes(value);
        return WebEncoders.Base64UrlEncode(tokenBytes);
    }

    public static string UrlDecode(this string value)
    {
        byte[] tokenBytes = WebEncoders.Base64UrlDecode(value);
        return Encoding.UTF8.GetString(tokenBytes);
    }
}