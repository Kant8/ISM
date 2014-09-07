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

                Console.Write("Enter key: ");
                var key = Console.ReadLine();
                if (String.IsNullOrEmpty(key))
                    break;

                coder.Key = key;

                try
                {

                    Console.WriteLine("\n\nEncoded message:" + Environment.NewLine);
                    var encodedMessage = coder.Encode(message);
                    Console.WriteLine(encodedMessage);

                    Console.WriteLine("\n\nDecoded message:" + Environment.NewLine);
                    var decodedMessage = coder.Decode(encodedMessage);
                    Console.WriteLine(decodedMessage);

                    Console.WriteLine("\n\nAre input and decoded equal? - " + (message == decodedMessage));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}
