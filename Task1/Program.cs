using System;
using System.IO;

namespace Task1
{
    internal class DeleteOld
    {
        static int Main(string[] args)
        {
            string? inputPath;
            if (args.Length == 0)
            {
                Console.WriteLine("Incert path to the folder for cleaning or use it as an argument next time you run programm.");
                inputPath = Console.ReadLine();
            }
            // In case we suppose to use arguments.
            else
            {
                inputPath = args[0];
            }

            if (!Directory.Exists(inputPath))
            {
                Console.WriteLine("Error: Directory you input does not exist!");
                // Had to remember my C programming experience.
                return 1;
            }

            string[] folders = Directory.GetDirectories(inputPath);
            foreach (string folder in folders)
            {
                Console.WriteLine(Directory.GetCreationTime(folder));
            }
            Console.WriteLine(inputPath);
            return 0;
        }
    }
}