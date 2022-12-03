namespace CaesarsSipher;

public static class Defaults
{
    public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string GetValidKey(string key, int length)
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
}