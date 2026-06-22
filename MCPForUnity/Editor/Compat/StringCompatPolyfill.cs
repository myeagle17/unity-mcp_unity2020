// String API polyfills for Unity 2020.3 (.NET Standard 2.0), which lacks the
// char / StringComparison overloads added in .NET Core 2.1 / netstandard 2.1.
// Placed in the GLOBAL namespace so the extension methods are visible everywhere
// without an explicit `using`. Disabled on Unity 2021.2+ where the BCL has them.
#if !UNITY_2021_2_OR_NEWER
using System;
using System.Text;

internal static class McpStringCompatExtensions
{
    public static bool Contains(this string s, char value)
        => s != null && s.IndexOf(value) >= 0;

    public static bool Contains(this string s, string value, StringComparison comparison)
        => s != null && value != null && s.IndexOf(value, comparison) >= 0;

    public static bool StartsWith(this string s, char value)
        => !string.IsNullOrEmpty(s) && s[0] == value;

    public static bool EndsWith(this string s, char value)
        => !string.IsNullOrEmpty(s) && s[s.Length - 1] == value;

    public static string Replace(this string s, string oldValue, string newValue, StringComparison comparison)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(oldValue)) return s;
        newValue = newValue ?? string.Empty;
        var sb = new StringBuilder();
        int prev = 0, idx;
        while ((idx = s.IndexOf(oldValue, prev, comparison)) >= 0)
        {
            sb.Append(s, prev, idx - prev);
            sb.Append(newValue);
            prev = idx + oldValue.Length;
        }
        sb.Append(s, prev, s.Length - prev);
        return sb.ToString();
    }
}
#endif
