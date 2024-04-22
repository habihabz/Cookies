﻿using Cookies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Services
{
    public interface IRoleMenu
    {
        RoleMenu GetMenuRole(int id);

        IEnumerable<RoleMenu> GetRoleMenus { get; }

        void Add(RoleMenu roleMenu);

        void Remove(int id);

        void Update(RoleMenu roleMenu);
        RoleMenu GetRoleMenusByRoleAndMenu(int role, int item);
        int getCountOfRoleMenuByRoleAndType(int role, string type);
        void RemoveAllRoleMenu(int role, string type);
    }
}
