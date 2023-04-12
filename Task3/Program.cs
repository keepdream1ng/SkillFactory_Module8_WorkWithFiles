using System;
using System.IO;
using static Task2.Task2;

namespace Task3
{
    internal class DeleteOldTask3 
    {
        static void Main(string[] args)
        {
            string? inputPath;
            TimeSpan TimeNotUsedFor = TimeSpan.FromMinutes(30);
            long DirectorySize;
            long CleanDirectorySize;
            int dirCount = 0;
            int fileCount = 0;

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

            DirectorySize = DirSize(inputPath);

            Console.WriteLine($"Folder size before cleaning {DirectorySize} bites");

            // Folders get checked if they are old or not and get deleted if they are.
            try
            {
                string[] folders = Directory.GetDirectories(inputPath);
                foreach (string folder in folders)
                {
                    // I found GetLastAccessTime for directories to work incorrectly, maybe its windows explorer fault.
                    // So I swithed to last write time for folders.
                    if (DateTime.Now.Subtract(Directory.GetLastWriteTime(folder)) > TimeNotUsedFor)
                    {
                        Directory.Delete(folder, true);
                        if (!Directory.Exists(folder)) dirCount++;
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
                        if (!File.Exists(file)) fileCount++;
                    }
                }

                CleanDirectorySize = DirSize(inputPath);
                Console.WriteLine($"Program deleted {dirCount} directories and {fileCount} files");
                Console.WriteLine($"Freed up disk space: {DirectorySize - CleanDirectorySize} bites");
                Console.WriteLine($"Current directory size: {CleanDirectorySize} bites");
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine("Error: No acces to the file");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}