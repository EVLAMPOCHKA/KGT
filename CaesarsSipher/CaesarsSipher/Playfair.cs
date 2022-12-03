namespace CaesarsSipher;

public static class Playfair
{
    private const string Key = "FROG";
    private const string LastName = "NEVEROVA";

    private static char[,] GetKeyTable(string key)
    {
        var newKey = key.Distinct(); 
        var newAlphabet = new List<char>();
        newAlphabet.AddRange(newKey);
        newAlphabet.AddRange(Defaults.Alphabet.Except(Key));
        char[,] keyTable = new Char[5, 5];
        int index = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                keyTable[i, j] = newAlphabet[index];
                index++;
                Console.Write("{0}\t", keyTable[i, j]);
            }

            Console.WriteLine();
        }

        return keyTable;
    }

    private static List<string> GetBigrams(string value)
    {
        var bigrams = new List<string>();

        for (var i = 0; i < value.Length; i++)
        {
            var bigram = value.Substring(i, value.Length - i == 1 ? 1 : 2);
            if (bigram.Length == 1 || bigram[0] == bigram[1])
            {
                bigram = $"{bigram[0]}{'X'}";
            }
            else
            {
                i++;
            }

            bigrams.Add(bigram);
        }

        return bigrams;
    }

    private static List<char> Sipher(List<string> bigrams, char[,] keyTable, bool isUnsipher = false)
    {
        var result = new List<char>();
        foreach (var bigram in bigrams)
        {
            var first = GetPosition(keyTable, bigram[0]);
            var second = GetPosition(keyTable, bigram[1]);
            if (first.Item1 == second.Item1)
            {
                result.Add(keyTable[first.Item1, (isUnsipher ? first.Item2 + 4 : first.Item2 + 1) % 5]);
                result.Add(keyTable[second.Item1, (isUnsipher ? second.Item2 + 4 : second.Item2 + 1) % 5]);
            }
            else
            {
                if (first.Item2 == second.Item2)
                {
                    result.Add(keyTable[(isUnsipher ? first.Item1 + 4 : first.Item1 + 1) % 5, first.Item2]);
                    result.Add(keyTable[(isUnsipher ? second.Item1 + 4 : second.Item1 + 1) % 5, second.Item2]);
                }
                else
                {
                    result.Add(keyTable[first.Item1, second.Item2]);
                    result.Add(keyTable[second.Item1, first.Item2]);
                }
            }
        }

        return result;
    }

    private static Tuple<int, int> GetPosition(char[,] keyTable, char symbol)
    {
        for (int x = 0; x < 5; ++x)
        {
            for (int y = 0; y < 5; ++y)
            {
                if (keyTable[x, y].Equals(symbol))
                    return Tuple.Create(x, y);
            }
        }

        return Tuple.Create(-1, -1);
    }

    public static void ShowWork()
    {
        var keyTable = GetKeyTable(Key);
        var bigrams = GetBigrams(LastName);
        var sipher = Sipher(bigrams, keyTable);
        Console.WriteLine($"Ciphertext is {string.Join(String.Empty, sipher)}");
        var sipherBigrams = GetBigrams(string.Join(String.Empty, sipher));
        var realLastName = Sipher(sipherBigrams, keyTable, true);
        Console.WriteLine($"Real text is {string.Join(String.Empty, realLastName)}");
    }
}