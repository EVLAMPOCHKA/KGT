using static System.String;

namespace CaesarsSipher;

public static class Viziner
{
    private const string Key = "WEDNESDAY";
    private const string Text = "NEVEROVA";

    private static char[,] GetAlphabetTable()
    {
        char[,] keyTable = new Char[26, 26];

        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                keyTable[i, j] = Defaults.Alphabet[(i + j) % 26];
                Console.Write("{0} ", keyTable[i, j]);
            }

            Console.WriteLine();
        }

        return keyTable;
    }

    private static string GetSipher(char[,] alphabetTable)
    {
        var validKey = Defaults.GetValidKey(Key, Text.Length);
        var result = Text.Select((t, i) =>
            alphabetTable[Defaults.Alphabet.IndexOf(t), Defaults.Alphabet.IndexOf(validKey[i])]).ToList();

        return Join(Empty, result);
    }

    public static void ShowWork()
    {
        var alphabetTable = GetAlphabetTable();
        Console.WriteLine($"Real text is {Text}");
        Console.WriteLine($"Sipher is {GetSipher(alphabetTable)}");
    }
}