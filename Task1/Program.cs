using System;
using System.IO;

namespace Task1
{
    internal class DeleteOld
    {
        static int Main(string[] args)
        {
            string? inputPath;
            TimeSpan TimeNotUsedFor = TimeSpan.FromMinutes(30);

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

            // Folders get checked if they are old or not and get deleted if they are.
            try
            {
                string[] folders = Directory.GetDirectories(inputPath);
                foreach (string folder in folders)
                {
                    if (DateTime.Now.Subtract(Directory.GetLastAccessTime(folder)) > TimeNotUsedFor)
                    {
                        Directory.Delete(folder, true);
                    }
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine("Error: No acces to the folder");
            }
            
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Files get checked if they are old or not and get deleted if they are.
            try
            {
                string[] files = Directory.GetFiles(inputPath);
                foreach (string file in files)
                {
                    if (DateTime.Now.Subtract(File.GetLastAccessTime(file)) > TimeNotUsedFor)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine("Error: No acces to the file");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return 0;
        }
    }
}