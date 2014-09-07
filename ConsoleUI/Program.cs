using System;
using Crypto;
using Crypto.Caesar;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Hello my name is Andrey." + Environment.NewLine
                             + "Привет, меня зовут Андрей." + Environment.NewLine
                             + "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" + Environment.NewLine
                             + "абвгдеёжзийклмнопрстуфхцчшщъыьэюя" + Environment.NewLine
                             + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + Environment.NewLine
                             + "abcdefghijklmnopqrstuvwxyz" + Environment.NewLine;

            ICryptoCoder coder;

            coder = new Caesar();

            while (true)
            {
                Console.WriteLine("\n-------------------------------------------------------\n");

                Console.WriteLine("Input message:\n");
                Console.WriteLine(message);

                Console.Write("Enter shift: ");
                var stringShift = Console.ReadLine();
                if (String.IsNullOrEmpty(stringShift))
                    break;
                int shift;
                if (!Int32.TryParse(stringShift, out shift))
                {
                    Console.WriteLine("Illegal input. Try again");
                    continue;
                }

                Console.WriteLine("\n\nEncoded message:" + Environment.NewLine);
                var encodedMessage = coder.Encode(message, shift);
                Console.WriteLine(encodedMessage);

                Console.WriteLine("\n\nDecoded message:" + Environment.NewLine);
                var decodedMessage = coder.Decode(encodedMessage, shift);
                Console.WriteLine(decodedMessage);

                Console.WriteLine("\n\nAre input and decoded equal? - " + (message == decodedMessage));
                Console.ReadLine();
            }
        }
    }
}
