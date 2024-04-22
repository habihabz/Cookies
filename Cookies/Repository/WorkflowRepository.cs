﻿using Cookies.Models;
using Cookies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Repository
{
    public class WorkflowRepository : IWorkflow
    {
        private DBContext db;

        public WorkflowRepository(DBContext _db)
        {
            db = _db;
        }
        public IEnumerable<Workflow> GetWorkflows => db.Workflows;

        public void Add(Workflow workflow)
        {
            db.Workflows.Add(workflow);
            db.SaveChanges();
        }

        public Workflow GetWorkflow(int id)
        {
            Workflow workflow = db.Workflows.Find(id);
            return workflow;
        }

        public void Remove(int id)
        {
            Workflow workflow = db.Workflows.Find(id);
            db.Workflows.Remove(workflow);
            db.SaveChanges();
        }

        public void Update(Workflow workflow1)
        {
            db.Workflows.Update(workflow1);
            db.SaveChanges();
        }
    }
}
