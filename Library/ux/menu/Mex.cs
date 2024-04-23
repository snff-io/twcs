public static class Mex
{
    public static IEnumerable<MenuOption> AsOptions(this IEnumerable<string> displayNames)
    {
        var cnt = 1;
        var options = new List<MenuOption>();
        foreach (var name in displayNames)
        {

            options.Add(new MenuOption
            {
                Display = name,                
                Ordinal = cnt

            });

            cnt++;
        }
        return options;
    }
}