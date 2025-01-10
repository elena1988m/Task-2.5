using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введіть ланцюг ДНК (тільки символи A, C, G, T):");
        string dnaChain = Console.ReadLine();

        byte[] compressed = Compress(dnaChain);
        Console.WriteLine("Стиснений ланцюг ДНК (у вигляді байтів): " + BitConverter.ToString(compressed));

        string decompressed = Decompress(compressed, dnaChain.Length);
        Console.WriteLine("Відновлений ланцюг ДНК: " + decompressed);
    }

    static byte[] Compress(string input)
    {
        int length = input.Length;
        int byteCount = (length + 3) / 4;
        byte[] compressed = new byte[byteCount];

        for (int i = 0; i < length; i++)
        {
            int byteIndex = i / 4;
            int bitOffset = (i % 4) * 2;

            byte encodedNucleotide = input[i] switch
            {
                'A' => 0b00,
                'C' => 0b01,
                'G' => 0b10,
                'T' => 0b11,
                _ => throw new ArgumentException("Недопустимий символ")
            };

            compressed[byteIndex] |= (byte)(encodedNucleotide << (6 - bitOffset));
        }

        return compressed;
    }

    static string Decompress(byte[] compressed, int originalLength)
    {
        StringBuilder result = new StringBuilder(originalLength);

        for (int i = 0; i < originalLength; i++)
        {
            int byteIndex = i / 4;
            int bitOffset = (i % 4) * 2;

            byte encodedNucleotide = (byte)((compressed[byteIndex] >> (6 - bitOffset)) & 0b11);

            char nucleotide = encodedNucleotide switch
            {
                0b00 => 'A',
                0b01 => 'C',
                0b10 => 'G',
                0b11 => 'T',
                _ => throw new ArgumentException("Помилка декомпресії")
            };

            result.Append(nucleotide);
        }

        return result.ToString();
    }
}
