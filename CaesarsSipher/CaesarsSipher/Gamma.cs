using System.Text;

namespace CaesarsSipher;

public static class Gamma
{
    private const string Text = "secret"; // текст для зашифровки
    private const string Key = "key"; // ключ для шифрования

    private static byte[] GetAsciiBytes(string value) // метод который переводит строку в массив байтов (каждая буква = какой-то байт в таблице АСКИ)
    {
        return Encoding.ASCII.GetBytes(value); // возвращаем этот массив
    }

    private static string GetValueByAsciiBytes(byte[] bytes) // метод который наоборот из массива байтов собирает слово
    {
        return Encoding.ASCII.GetString(bytes); // возвращает это слово
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
        var textInBytes = GetAsciiBytes(Text);
        var keyInBytes = GetAsciiBytes(Defaults.GetValidKey(Key, Text.Length));
        var sipher = GetSipher(textInBytes, keyInBytes);
        Console.WriteLine($"Real text: {Text}");
        Console.WriteLine($"Key: {Key}");
        Console.WriteLine($"Sipher: {GetValueByAsciiBytes(sipher)}");
        var realText = GetValueByAsciiBytes(GetSipher(sipher, GetAsciiBytes(Defaults.GetValidKey(Key, Text.Length))));
     
        Console.WriteLine($"Real text: {realText}");
    }
}