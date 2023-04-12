using System;
using System.Collections.Generic;
using System.Text;

namespace FinalTask
{
    public class Group
    {
        public string Name { get; set; }
        public static List<Group> GroupList = new();
        public List<Student> StudentsInGroup = new();

        public Group(string name)
        {
            Name = name;
            Group.GroupList.Add(this);
        }

        private static Group GetGroup(string name)
        {
            foreach (Group g in GroupList)
            {
                if (g.Name == name) return g;
            }
            return new Group(name);
        }

        public static void GroupStudent(Student student)
        {
            Group ToGroupIn = GetGroup(student.Group);
            ToGroupIn.StudentsInGroup.Add(student);
        }
       
        public static void GroupStudentsArr(Student[] students)
        {
            foreach (Student student in students)
            {
                GroupStudent(student);
            }
        }

        public StringBuilder GetGroupListString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Student student in this.StudentsInGroup)
            {
                sb.AppendLine(student.ToString());
            }
            return sb;
        }
    }
}
