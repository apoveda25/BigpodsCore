using System.Linq;
using System.Text.RegularExpressions;

namespace Bigpods.MonolithIaC.Utils;

public class StringUtils
{
    public static string ToCapitalCase(string s) => char.ToUpper(s[0]) + s[1..].ToLower();
    public static string ToCapitalCase(string[] s) => string.Join(" ", s.Select(s => ToCapitalCase(s)));

    public static string ToKebabCase(string s)
    {
        return string.Join(
            "",
            Regex.Replace(s.Trim(), @"\s+", "-")
                .Select((x, i) =>
                {
                    if (i == 0) return $"{char.ToLower(x)}";

                    return char.IsUpper(x) && char.IsLower(s[i - 1]) ? $"-{char.ToLower(x)}" : $"{char.ToLower(x)}";
                })
        );
    }
}
