using System.Linq;

namespace Bigpods.MonolithIaC.Utils;

public class StringUtils
{
    public static string ToCapitalCase(string s) => char.ToUpper(s[0]) + s[1..].ToLower();
    public static string ToCapitalCase(string[] s) => string.Join(" ", s.Select(s => ToCapitalCase(s)));
}
