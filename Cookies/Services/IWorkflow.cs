using Cookies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Services
{
    public interface IWorkflow
    {
        IEnumerable<Workflow> GetWorkflows { get; }
        Workflow GetWorkflow(int id);

        void Add(Workflow workflow);

        void Remove(int id);
        void Update(Workflow workflow1);
    }
}
