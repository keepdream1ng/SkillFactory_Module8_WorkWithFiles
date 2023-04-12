using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class FinalTask 
    {
        static void Main(string[] args)
        {
            string? inputPath;

            if (args.Length == 0)
            {
                Console.WriteLine("Incert path to the binary file or use it as an argument next time you run programm.");
                inputPath = Console.ReadLine();
            }
            // In case we suppose to use arguments.
            else
            {
                inputPath = args[0];
            }

            try
            {
                if (!File.Exists(inputPath))
                {
                    throw new FileNotFoundException($"Error, file {inputPath} does not exist");
                }
                BinaryFormatter formatter = new();
                using (var fs = new FileStream(inputPath, FileMode.Open))
                {
                    Student[] Students = (Student[])formatter.Deserialize(fs);

                    // For now it prints deserialized objects.
                    foreach (var student in Students)
                    {
                        Console.WriteLine(student);
                    }
                }

                DirectoryInfo StudentsFolder = CreateDesktopDir("Students");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static DirectoryInfo CreateDesktopDir(string dir)
        {
            string NewPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),dir);
            if (Directory.Exists(NewPath))
            {
                return new DirectoryInfo(NewPath);
            }
            return Directory.CreateDirectory(NewPath);
        }
    }

    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"{Name}, {DateOfBirth}";
        }
    }
}