using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Введіть ланцюг ДНК (тільки символи A, C, G, T):");
        string dnaChain = Console.ReadLine();

        string compressed = Compress(dnaChain);
        Console.WriteLine("Стиснений ланцюг ДНК: " + compressed);

        string decompressed = Decompress(compressed);
        Console.WriteLine("Відновлений ланцюг ДНК: " + decompressed);
    }

    static string Compress(string input)
    {
        string result = "";
        int count = 1;

        for (int i = 1; i <= input.Length; i++)
        {
            if (i < input.Length && input[i] == input[i - 1])
            {
                count++;
            }
            else
            {
                result += input[i - 1] + count.ToString();
                count = 1;
            }
        }
        return result;
    }

    static string Decompress(string input)
    {
        string result = "";
        for (int i = 0; i < input.Length; i += 2)
        {
            char nucleotide = input[i]; 
            int count = int.Parse(input[i + 1].ToString()); 

            for (int j = 0; j < count; j++)
            {
                result += nucleotide; 
            }
        }
        return result;
    }
}