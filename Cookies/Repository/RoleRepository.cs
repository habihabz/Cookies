﻿using Microsoft.EntityFrameworkCore.Internal;
using Cookies.Models;
using Cookies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Repository
{
    public class RoleRepository : IRole
    {
        private readonly DBContext db;

        public RoleRepository(DBContext _db)
        {
            db = _db;
        }

        public IEnumerable<Role> GetRoles => db.Roles;

        public void Add(Role role)
        {
            db.Roles.Add(role);
            db.SaveChanges();
        }

        public Role GetRole(int id)
        {
            Role role = db.Roles.Find(id);
            return role;
        }

        public void Remove(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
        }

        public void Update(Role role1)
        {
            db.Update(role1);
            db.SaveChanges();
        }
    }
}
