using System;
using System.IO;

namespace Task2
{
    internal class Task2 
    {
        static void Main(string[] args)
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

            Console.WriteLine($"Directory {inputPath} size is {DirSize(inputPath)} bites.");
        }

        /// <summary>
        /// Recursively counts size of the directory and all files and subdirectories.
        /// </summary>
        public static long DirSize(string path)
        {
            long size = 0;
            try
            {
                if (!Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException($"Error: Directory {path} does not exist!");
                }
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                FileInfo[] files = dirInfo.GetFiles();
                foreach (FileInfo fInfo in files)
                {
                    size += fInfo.Length;
                }

                DirectoryInfo[] subDirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo dir in subDirs)
                {
                    size += DirSize(dir);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return size;
        }

        public static long DirSize(DirectoryInfo dirInfo)
        {
            long size = 0;
            try
            {
                FileInfo[] files = dirInfo.GetFiles();
                foreach (FileInfo fInfo in files)
                {
                    size += fInfo.Length;
                }

                DirectoryInfo[] subDirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo dir in subDirs)
                {
                    size += DirSize(dir);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return size;
        }
    }
}