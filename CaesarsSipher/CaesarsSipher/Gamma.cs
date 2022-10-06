using System.Text;

namespace CaesarsSipher;

public static class Gamma
{
    private const string Text = "Neverova";
    private const string Key = "Frog";

    private static byte[] GetAsciiBytes(string value)
    {
        return Encoding.ASCII.GetBytes(value);
    }

    private static string GetValueByAsciiBytes(byte[] bytes)
    {
        return Encoding.ASCII.GetString(bytes);
    }
    
    private static string GetValidKey(string key, int length)
    {
        if (key.Length >= length)
        {
            return key;
        }
        var newKey = key;
        for(var i = 0; i<length - key.Length; i++)
        {
            newKey += key[i % key.Length];
        }

        return newKey;
    }

    private static byte[] GetSipher(byte[] text, byte[] key)
    {
        var sipher = new List<byte>();
        for (var i = 0; i < text.Length; i++)
        {
            sipher.Add((byte)(text[i]^key[i]));
        }

        return sipher.ToArray();
    }

    public static void ShowWork()
    {
        var sipher = GetSipher(GetAsciiBytes(Text), GetAsciiBytes(GetValidKey(Key, Text.Length)));
        Console.WriteLine($"Real text: {Text}");
        Console.WriteLine($"Sipher: {GetValueByAsciiBytes(sipher)}");
        var realText = GetSipher(sipher, GetAsciiBytes(GetValidKey(Key, Text.Length)));
        Console.WriteLine($"Real text: {GetValueByAsciiBytes(realText)}");
    }
}