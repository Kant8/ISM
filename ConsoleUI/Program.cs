using System;
using System.Text;
using Crypto;
using Crypto.Stream;

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

            ICryptoCoder coder = null;

            coder = new Scrambler();

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
                    var encodedMessage = coder.Encode(Encoding.Unicode.GetBytes(message));
                    Console.WriteLine(Encoding.Unicode.GetString(encodedMessage));

                    Console.WriteLine("\n\nDecoded message:" + Environment.NewLine);
                    var decodedMessage = coder.Decode(encodedMessage);
                    Console.WriteLine(Encoding.Unicode.GetString(decodedMessage));

                    Console.WriteLine("\n\nAre input and decoded equal? - " 
                        + (message == Encoding.Unicode.GetString(decodedMessage)));
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
