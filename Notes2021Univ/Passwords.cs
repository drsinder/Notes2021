using System;
using System.Collections.Generic;
using System.Text;

namespace Notes2021Univ
{
    public static class Passwords
    {
        /// <summary>
        /// Gets the console secure password.
        /// </summary>
        /// <returns></returns>
        public static string GetConsoleSecurePassword()
        {
            StringBuilder pwd = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    //pwd.Remove(pwd.Length - 1);
                    //Console.Write("\b \b");
                    pwd = new StringBuilder();
                    Console.WriteLine();
                }
                else
                {
                    pwd.Append(i.KeyChar);
                    Console.Write("*");
                }
            }
            return pwd.ToString();
        }

        /// <summary>
        /// Gets the console password.
        /// </summary>
        /// <returns></returns>
        public static string GetConsolePassword()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        sb.Length--;
                    }

                    continue;
                }

                Console.Write('*');
                sb.Append(cki.KeyChar);
            }

            return sb.ToString();
        }
    }
}
