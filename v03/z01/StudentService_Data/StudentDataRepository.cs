﻿using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;

namespace StudentService_Data
{
    public class StudentDataRepository
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public StudentDataRepository()
        {
            _storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
            Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("StudentTable");
            _table.CreateIfNotExists();
        }

        public IQueryable<Student> RetrieveAllStudents()
        {
            var results = from g in _table.CreateQuery<Student>()
                          where g.PartitionKey == "Student"
                          select g;
            return results;
        }

        public void AddStudent(Student newStudent)
        {
            // TODO
            // Samostalni rad: izmestiti tableName u konfiguraciju servisa.
            TableOperation insertOperation = TableOperation.Insert(newStudent);
            _table.Execute(insertOperation);
        }
    }
}
