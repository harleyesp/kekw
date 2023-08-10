using System;
using System.Threading;

namespace HelloWorldApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string greeting = "Hello, ";
            string target = "World";
            string punctuation = "!";
            
            Console.Write(greeting);
            TypeWriterEffect(target);
            Console.WriteLine(punctuation);
            
            Console.WriteLine("The current date is: " + DateTime.Now.ToString("yyyy-MM-dd"));
            Console.WriteLine("The time is: " + DateTime.Now.ToString("HH:mm:ss"));
            
            int count = 0;
            while (count < 3)
            {
                Console.WriteLine("Printing again...");
                count++;
            }
            
            Console.WriteLine("Calculating some meaningless math:");
            int result = 10 * 5 + 3 / 2 - 7;
            Console.WriteLine("Result: " + result);
            
            Console.WriteLine("Generating random numbers:");
            Random random = new Random();
            for (int j = 0; j < 5; j++)
            {
                Console.WriteLine("Random number " + (j + 1) + ": " + random.Next(1, 100));
            }
            
            Console.WriteLine("And that's the end of the program!");
        }
        
        static void TypeWriterEffect(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                Thread.Sleep(100); // Adjust the delay to control typing speed
            }
            
            Console.WriteLine(); // Move to the next line after typing is complete
        }
    }
}
