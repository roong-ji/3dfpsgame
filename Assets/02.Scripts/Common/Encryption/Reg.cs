using System.Text.RegularExpressions;

public static class Reg
{
    public const int MinLength = 7;
    public const int MaxLength = 20;

    public static bool IsEmailType(string input)
    {
        return Regex.IsMatch(input, @"^[a-zA-Z0-9]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
    }

    public static bool IsAllowedChars(string input)
    {
        return Regex.IsMatch(input, @"^[a-zA-Z0-9!@#$%^&*()_+=.-]+$");
    }

    public static bool IsAllowedLength(string input)
    {
        return Regex.IsMatch(input, $@"^.{{{MinLength},{MaxLength}}}$");
    }

    public static bool HasSpecialChar(string input)
    {
        return Regex.IsMatch(input, @"[!@#$%^&*()_+=.-]");
    }

    public static bool HasUpperAndLower(string input)
    {
        return Regex.IsMatch(input, @"(?=.*[a-z])(?=.*[A-Z])");
    }

    public static bool IsValidPassword(string input)
    {
        return Regex.IsMatch(input, $@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+=.-])[a-zA-Z0-9!@#$%^&*()_+=.-]{{{MinLength},{MaxLength}}}");
    }
}
