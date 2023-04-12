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
                Student[] Students;
                // Binary file has an student array in it, we are getting it inside using.
                using (var fs = new FileStream(inputPath, FileMode.Open))
                {
                    Students = (Student[])formatter.Deserialize(fs);
                }
 
                // Next method creates Group objects based on Student properties.
                Group.GroupStudentsArr(Students);
                DirectoryInfo StudentsFolder = CreateDesktopDir("Students");

                // Now we can create a file for each group.
                foreach (Group group in Group.GroupList)
                {
                    inputPath = Path.Combine(StudentsFolder.FullName, group.Name);
                    using (StreamWriter sw = File.CreateText($"{inputPath}.txt"))
                    {
                        sw.Write(group.GetGroupListString());
                    }
                    Console.WriteLine($"File {inputPath}.txt is created.");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static DirectoryInfo CreateDesktopDir(string dir)
        {
            string NewPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),dir);
            if (Directory.Exists(NewPath))
            {
                return new DirectoryInfo(NewPath);
            }
            return Directory.CreateDirectory(NewPath);
        }
    }
}