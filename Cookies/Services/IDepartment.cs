﻿using Cookies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Services
{
    public interface IDepartment
    {
        IEnumerable<Department> GetDepartments { get; }
        Department GetDepartment(int id);

        void Add(Department department);

        void Remove(int id);

        void Update(Department department);
    }
}
