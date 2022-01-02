using System.IO;
using System.Text;

public static class JuceCoreStringExtensions
{
    public static string FirstCharToUpper(this string str)
    {
        if(string.IsNullOrEmpty(str))
        {
            return str;
        }

        StringBuilder stringBuilder = new StringBuilder(str);
        stringBuilder[0] = char.ToUpper(stringBuilder[0]);

        return stringBuilder.ToString();
    }

    public static Stream ToStream(this string str)
    {
        MemoryStream ret = new MemoryStream();

        StreamWriter writer = new StreamWriter(ret);
        writer.Write(str);
        writer.Flush();

        ret.Position = 0;

        return ret;
    }
}