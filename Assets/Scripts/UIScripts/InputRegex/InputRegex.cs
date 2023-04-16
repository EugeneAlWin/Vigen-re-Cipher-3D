using System.Text.RegularExpressions;

internal static class InputRegex
{
    internal static readonly Regex latMessage = new(@"^[a-z]*$");
    internal static readonly Regex cyrMessage = new(@"^([�-�]|�)*$");
    internal static readonly Regex latKey = new(@"^[a-z]*$");
    internal static readonly Regex cyrKey = new(@"^([�-�]|�)*$");
    internal static readonly Regex Depth = new(@"^[0-9]*$");
    internal static readonly Regex Step = new(@"^[0-9]*$");

    internal static bool IsMatch(string text, Regex regex) => regex.IsMatch(text);
}