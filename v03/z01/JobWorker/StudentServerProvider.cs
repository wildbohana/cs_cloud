using Contracts;
using StudentService_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO ovo rešenje ne koristi Table
// Napraviti ga tako da koristi Table

namespace JobWorker
{
    public class StudentServerProvider : IStudent
    {
        private List<Student> students;

        public StudentServerProvider()
        {
            students = new List<Student>();
        }

        public void AddStudent(string indexNo, string name, string lastName)
        {
            Student s = new Student(indexNo);
            s.Name = name;
            s.LastName = lastName;
            students.Add(s);
        }

        public List<Student> RetrieveAllStudents()
        {
            return students;
        }
    }
}
