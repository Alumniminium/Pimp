using System;

namespace pimp.Helpers
{
    public static class StringExt
    {
        public static string ToLength(this string str, int length, bool appendBefore = false)
        {
            if (str.Length < length)
            {
                var cpy = str;
                while (cpy.Length < length)
                {
                    if (appendBefore)
                        cpy = " " + cpy;
                    else
                        cpy += " ";
                }
                return cpy;
            }
            else if (str.Length > length)
            {
                return str.Substring(0, length);
            }
            else
            {
                return str;
            }
        }
    }
}