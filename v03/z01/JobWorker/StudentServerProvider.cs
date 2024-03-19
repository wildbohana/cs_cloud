using Contracts;
using StudentService_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class StudentServerProvider : IStudent
    {
        private StudentDataRepository repo;

        public StudentServerProvider()
        {
            repo = new StudentDataRepository();
        }

        public void AddStudent(string indexNo, string name, string lastName)
        {
            Student s = new Student(indexNo);
            s.Name = name;
            s.LastName = lastName;

            repo.AddStudent(s);
        }

        public List<Student> RetrieveAllStudents()
        {
            return repo.RetrieveAllStudents().ToList();
        }
    }
}
