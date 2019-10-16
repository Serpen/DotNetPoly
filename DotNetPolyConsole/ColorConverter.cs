using System;

namespace DotNetPolyConsole
{
    public class ColorConverter
    {
        private static System.Collections.Generic.Dictionary<System.Drawing.Color, ConsoleColor> MatchingCache = new System.Collections.Generic.Dictionary<System.Drawing.Color, ConsoleColor>();

        internal static ConsoleColor ClosestConsoleColor(System.Drawing.Color color)
        {
            ConsoleColor ret;

            if (MatchingCache.TryGetValue(color, out ret))
                return ret;

            double rr = color.R;
            double gg = color.G;
            double bb = color.B;
            var delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var name = Enum.GetName(typeof(ConsoleColor), cc);
                var c = System.Drawing.Color.FromName(System.Convert.ToString(name == "DarkYellow" ? "Orange" : name));
                var t = Math.Pow(c.R - rr, 2) + Math.Pow(c.G - gg, 2) + Math.Pow(c.B - bb, 2);
                if (t == 0)
                {
                    if (!MatchingCache.ContainsKey(color))
                        MatchingCache.Add(color, cc);
                    return cc;
                }
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            if (!MatchingCache.ContainsKey(color))
                MatchingCache.Add(color, ret);
            return ret;
        }
    }
}
