namespace MusicStore.Application.Utils;

public class ResponseMessages
{
    public static string Successfuly
    {
        get { return "Operation success."; }
    }

    public static string Failed
    {
        get { return "Operation failed"; }
    }

    public static string UnmatchedPasswords
    {
        get { return "Passwords must be same!"; }
    }

    public static string UnvalidEmail
    {
        get { return "Mail format not valid."; }
    }

    public static string TakenEmail
    {
        get { return "This mail was already taken."; }
    }
    
    public static string UnvalidLogin
    {
        get { return "Invalid login info."; }
    }
}