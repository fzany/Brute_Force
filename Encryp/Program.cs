using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encryp
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            origin:
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = DateTime.UtcNow;
            double counter = 0;
            Console.WriteLine("Enter text to brute");
            string match = Console.ReadLine();
            Console.WriteLine("Press any key to start");
            Console.ReadKey();

            string[] alphabeth = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                    "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "=", "+",
                                    "[", "]", "{", "}", ";", ":", ",", "<", ">", "?", "/", " ", "|", @"\"};
            string result = string.Empty;
            bool reversed = false;
            StringBuilder sob = new StringBuilder();
            foreach (string item in alphabeth)
            {
                sob.Append(item);
            }
            again:
          
            //Console.WriteLine(sob.ToString());
            string format = sob.ToString();
          
            int lenght = format.Length;
            int starter = 0;
            int lenghter = 1;
            int ender = 0;
            bool isMultiple = false;
            start:
            if (lenghter > (lenght - starter))
            {
                ender = lenght - starter;
                isMultiple = true;
            }
            else
            {
                ender = lenghter;
                isMultiple = false;
            }
            result = EndConverter(format.Substring(starter, ender), isMultiple, lenghter);
            counter++;
           
            //Console.Clear();
            // Console.WriteLine(counter);

            if (result == match)
            {
                Console.WriteLine("match found");
                Console.WriteLine(result);
                endDate = DateTime.UtcNow;
                Console.WriteLine($"Search started: {startDate.ToLongDateString()}, {startDate.ToLongTimeString()}");
                Console.WriteLine($"Search finished: {endDate.ToLongDateString()}, {endDate.ToLongTimeString()}");
                double minutesTaken = (endDate - startDate).TotalMinutes;
                Console.WriteLine($"Search took: {minutesTaken} minutes and {counter} counts");

                Console.WriteLine("Press any key to start again.");
                Console.ReadKey();
                goto origin;
            }
            else
            {
                Console.WriteLine(result);
                starter++;
                if (starter >= lenght)
                {
                    starter = 0;
                    lenghter++;
                }

                if (lenghter <= lenght)
                {
                    goto start;
                }
                else
                {
                    if (!reversed)
                    {
                        reversed = true;
                        alphabeth = alphabeth.Reverse().ToArray();
                        sob.Clear();
                        foreach (string item in alphabeth)
                        {
                            sob.Append(item);
                        }
                        goto again;
                    }
                    else
                    {
                        reversed = false;
                        //scramble the list
                        List<string> temp1 = new List<string>();
                        while (temp1.Distinct().Count() != alphabeth.Count())
                        {
                            string ins = alphabeth[GetRandomNumber(0, alphabeth.Count())];
                            if (!temp1.Contains(ins))
                            {
                                temp1.Add(ins);
                            }
                        }
                        alphabeth = temp1.ToArray();
                        sob.Clear();
                        foreach (string item in alphabeth)
                        {
                            sob.Append(item);
                        }
                        goto again;
                    }

                }
            }



            Console.ReadKey();
        }
        public static readonly Random getrandom = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }
        private static string EndConverter(string v, bool isMultiple, int lenghter)
        {
            if (isMultiple)
            {
                string selected = v.Substring((v.Length - 1), 1);
                StringBuilder sob = new StringBuilder();
                sob.Append(v);
                for (int j = 0; j < lenghter - v.Length; j++)
                {
                    sob.Append(selected.ToString());
                }

                return sob.ToString();
            }
            return v;
        }
    }
}
