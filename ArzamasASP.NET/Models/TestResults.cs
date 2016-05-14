using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tests;
using Tests.Database;

namespace ArzamasASP.NET.Models
{
    public class TestResults
    {
        public IEnumerable<Run> Runs { get; set; }
        public IEnumerable<TestExecution> TestExecutions { get; set; }

    }
}