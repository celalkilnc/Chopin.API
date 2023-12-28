using System.Text.RegularExpressions;

namespace MusicStore.Application.Utils;

public class FormatChecker
{
    public static bool IsValidEmail(string email)
    { 
        return Regex.IsMatch(email, @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
    }
}