﻿using DAO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class EmployeeListResult
    {
        public IEnumerable<Employee> Employees { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredRecords { get; set; }
    }

}
