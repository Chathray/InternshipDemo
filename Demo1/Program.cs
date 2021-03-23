using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loop = true;
            float result = 0;

            while (loop)
            {
            begin:
                Console.Write("\nPress +, -, x or : to set your operator: ");
                char operato = Console.ReadKey().KeyChar;

                if (operato != '+' &
                    operato != '-' &
                    operato != 'x' &
                    operato != ':') goto begin;

                Console.Write("\nEnter a number: ");
                int n1 = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter another number: ");
                int n2 = Convert.ToInt32(Console.ReadLine());

                switch (operato)
                {
                    case '+':
                        result = n1 + n2;
                        break;
                    case '-':
                        result = n1 - n2;
                        break;
                    case 'x':
                        result = n1 * n2;
                        break;
                    case ':':
                        result = n1 / n2;
                        break;
                    default:
                        Console.WriteLine("The operator is not supported!");
                        break;
                }
                Console.Write("{0} {3} {1} = {2}\n", n1, n2, result, operato);

                Console.WriteLine("Press Enter to continues...\n");
                loop = Console.ReadKey().KeyChar == 13;
            }
        }
    }
}
