namespace MusicStore.API.Utils;

public class ResponseMessages
{
    public static string Success
    {
        get { return "Operation success"; }
    }

    public static string Failed
    {
        get { return "Operation failed"; }
    }

    public static string UnmatchedPassword
    {
        get { return "Passwords must be the same"; }
    }

    public static string UnvalidEmail
    {
        get { return "Unvalid mail format"; }
    }
    
    public static string UnmatchedCredentials
    {
        get { return "Unmatched user. Please check your credentials"; }
    }
}