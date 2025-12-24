using System.Text.RegularExpressions;

public static class Reg
{
    private static readonly Regex s_emailRegex
            = new Regex(@"^[a-zA-Z0-9-_%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

    private static readonly Regex s_allowedCharsRegex
        = new Regex(@"^[a-zA-Z0-9!@#$%^&*()_+=.-]+$", RegexOptions.Compiled);

    private static readonly Regex s_lengthRegex
        = new Regex($@"^.{{{MinLength},{MaxLength}}}$", RegexOptions.Compiled);

    private static readonly Regex s_specialCharRegex
        = new Regex(@"[!@#$%^&*()_+=.-]", RegexOptions.Compiled);

    private static readonly Regex s_upperLowerRegex
        = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])", RegexOptions.Compiled);

    private static readonly Regex s_validPasswordRegex
        = new Regex($@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+=.-])[a-zA-Z0-9!@#$%^&*()_+=.-]{{{MinLength},{MaxLength}}}$", RegexOptions.Compiled);

    public const int MinLength = 7;
    public const int MaxLength = 20;

    public static bool IsEmailType(string input)
    {
        return s_emailRegex.IsMatch(input);
    }

    public static bool IsAllowedChars(string input)
    {
        return s_allowedCharsRegex.IsMatch(input);
    }

    public static bool IsAllowedLength(string input)
    {
        return s_lengthRegex.IsMatch(input);
    }

    public static bool HasSpecialChar(string input)
    {
        return s_specialCharRegex.IsMatch(input);
    }

    public static bool HasUpperAndLower(string input)
    {
        return s_upperLowerRegex.IsMatch(input);
    }

    public static bool IsValidPassword(string input)
    {
        return s_validPasswordRegex.IsMatch(input);
    }
}
