namespace CaesarsSipher;

public static class Pgp
{
    private const string Symbols = "aбвгдежзийклмноп";
    private const long X = 179357333; // 11 variant

    private static string ToBinary(long value)
    {
        return Convert.ToString(value, 2);
    }

    private static Dictionary<char, string> GetBinaryCodes(string symbols)
    {
        var result = new Dictionary<char, string>();
        for (var index = 0; index < symbols.Length; index++)
        {
            var binary = ToBinary(index);
            result.Add(symbols[index],
                binary.Length == 1 ? "000" + binary :
                binary.Length == 2 ? "00" + binary :
                binary.Length == 3 ? "0" + binary : binary);
        }

        return result;
    }

    private static string GenerateMessage(Dictionary<char, string> dictionary)
    {
        var result = string.Empty;
        var random = new Random();

        for (var i = 0; i < 32; i++)
        {
            int index = random.Next(Symbols.Length);
            dictionary.TryGetValue(Symbols[index], out var binaryResult);
            result += binaryResult;
        }

        return result;
    }

    private static ulong ToDecimal(string value)
    {
        return (ulong)Convert.ToInt64(value, 2);
    }

    private static string GetValueAfterPledge(string valueIn28Bits)
    {
        return valueIn28Bits.Substring(5, valueIn28Bits.Length - 5) + valueIn28Bits.Substring(0, 5);
    }

    public static void ShowWork()
    {
        Console.WriteLine($"3^43 is {ToBinary(BitConverter.DoubleToInt64Bits(Math.Pow(3, 43)))} in binary");
        var result = GenerateMessage(GetBinaryCodes(Symbols));
        Console.WriteLine(result);
        Console.WriteLine(
            $"Binary value: {result.Substring(0, 64)} - Decimal value: {ToDecimal(result.Substring(0, 64))}");
        Console.WriteLine(
            $"Binary value: {result.Substring(64, 64)} - Decimal value: {ToDecimal(result.Substring(64, 64))}");
        Console.WriteLine($"{ToBinary(Convert.ToInt32(X))} => {GetValueAfterPledge(ToBinary(Convert.ToInt32(X)))}");
        Console.WriteLine($"{X} => {ToDecimal(GetValueAfterPledge(ToBinary(Convert.ToInt32(X))))}");
    }
}