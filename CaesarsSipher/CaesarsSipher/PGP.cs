namespace CaesarsSipher;

public static class Pgp
{
    private const string Symbols = "aбвгдежзийклмноп"; // первые 16 букв русского алфавита, они нужны по заданию
    private const int X = 179357333; // число, которое соответствует 11 варианту. Если у тебя другой - меняй на свое
    private const long Value = 22448993011; // число, которое дано по заданию

    private static string ToBinary(long value) // метод, который переводит число в двоичное представление (напр 3 это 10)
    {
        return Convert.ToString(value, 2); // возвращаем это двочиное представление в виде строки (напр "01101001001...")
    }

    private static Dictionary<char, string> GetBinaryCodes(string symbols) 
    // по заданию каждой букве русского алфавита (из тех, что в задании) должен соответствовать бинарный четырёхразрядный код
    // например буква "А" - у нее индекс 0 (т.к. она первая). 0 в двоичном это 0. Но это лишь один разряд, а нам надо 4,
    // поэтому мы добавляем в начало ещё 3 нуля и получается, что "А" = "0000" ("Б"="0001" и т.д.)
    {
        var result = new Dictionary<char, string>(); //объявляем пустой словарь, который потом заполним парами "А" = "0000"("Б"="0001" и т.д.)
        for (var index = 0; index < symbols.Length; index++) //проходимся по всем буквам в цикле
        {
            var binary = ToBinary(index); // находим двоичное представление буквы
            result.Add(symbols[index], //добавляем в словарь ключ букву(например А)
                binary.Length switch //добавляем значение в двоичном виде, но с проверкой, чтобы было 4 разряда
                {
                    1 => $"000{binary}", //это если длина представления 1 разряд(как я выше с 0 объсняла, поэтому тут 3 нуля в начало добавляется)
                    2 => $"00{binary}", //это если длина представления 2 разряда (напр мы получили 10, значит добавим ещё 2 нуля и будет 0010)
                    3 => $"0{binary}",// //это если длина представления 3 разряда(напр мы получили 111, значит добавим 1 ноль и будет 0111)
                    _ => binary// это дефолтное значение, выполняется если длина и так нас устраивает, т.е. если мы получили 1111, то это добавляем
                });
        }

        return result; // возвращаем заполненный словарь
    }

    private static string GenerateMessage(Dictionary<char, string> dictionary) // метод который генерирует рандомное сообщение из 32 букв
    {
        var result = string.Empty; // пока что наше сообщение пустое, объявили его и присвоили ему "".
        var random = new Random(); // метод чтобы рандомно выбрать число из диапазона

        for (var i = 0; i < 32; i++) // проходим в цикле 32 раза, т.к. нам надо 32 буквы
        {
            int index = random.Next(Symbols.Length); // выбираем рандомное число в пределах длины алфавита, т.е. от 0 до 16(напр 5)
            dictionary.TryGetValue(Symbols[index], out var binaryResult); // получаем из нашего словаря двоичное представление (напр 5 буква это 0101) 
            result += binaryResult; //добавляем к нашей строке результата двоичное представление (напр теперь результат не "", а "0101")
        }

        return result; // возвращаем эту длинную строку с сообщением в двоичном виде
    }

    private static ulong ToDecimal(string value) // метод который строку из 0 и 1 переводит в положительное число в десятичной системе
    {
        return (ulong)Convert.ToInt64(value, 2);
    }

    private static string GetValueAfterPledge(string valueIn28Bits) // метод который в строке из 28 0 и 1 делает циклический побитовый сдвиг влево.
        // т.к. нет типа данных для 28 бит, я просто в строке обрезаю первые 5 символов и вставляю их в конец. Это все поисходит в строке снизу.
    {
        return string.Concat(valueIn28Bits.AsSpan(5, valueIn28Bits.Length - 5), valueIn28Bits[..5]); // возвращаем преобразованное значение
    }

    private static long GetSumByMod(long value1, int value2) // находим сумму чисел и ее остаток от деления на 2 
    {
        return (value1 + value2) % 2; // возвращаем остаток от деления на 2
    }
    public static void ShowWork()
    {
        // по заданию возводим 3 в 43 степень и выводим результат в двоичном виде. (метод Pow возводит в любую сетепень)
        Console.WriteLine($"3^43 is {ToBinary(BitConverter.DoubleToInt64Bits(Math.Pow(3, 43)))} in binary"); 
        var result = GenerateMessage(GetBinaryCodes(Symbols)); // генерируем тут рандомное сообщение
        Console.WriteLine(result); // выводи это рандомное сообщение
        //сейчас выведем первую половину этого сообщения, т.е. 64 символа (напр "01001010110..." и его значение в десятичной системе)
        Console.WriteLine(
            $"Binary value: {result[..64]} - Decimal value: {ToDecimal(result[..64])}");
        //сейчас выведем вторую половину этого сообщения, т.е. оставшиеся 64 символа  (напр "01001010110..." и его значение в десятичной системе)
        Console.WriteLine(
            $"Binary value: {result.Substring(64, 64)} - Decimal value: {ToDecimal(result.Substring(64, 64))}");
        // тут число, которое дали по заданию выводим в двоичном виде и сразу же выводи его значение после сдвига (все в двичном виде)
        Console.WriteLine($"{ToBinary(Convert.ToInt32(X))} => {GetValueAfterPledge(ToBinary(Convert.ToInt32(X)))}");
        // тут тоже самое только в десятичном представлении
        Console.WriteLine($"{X} => {ToDecimal(GetValueAfterPledge(ToBinary(Convert.ToInt32(X))))}");
        // тут выводим остаток от деления суммы двух чисел на 2
        Console.WriteLine($"({Value} + {X}) mod 2 = {GetSumByMod(Value, X)}");
    }
}
