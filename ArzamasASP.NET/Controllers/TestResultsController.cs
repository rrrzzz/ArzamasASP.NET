using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ArzamasASP.NET.Models;
using Tests;
using Tests.Database;

namespace ArzamasASP.NET.Controllers
{
    public class TestResultsController : Controller
    {
        private static List<TestExecution> _executions;
        // GET: TestResults
        public ActionResult Index()
        {
            using (var db = new TestStatsContext())
            {
                var viewModel = new TestResults
                {
                    Runs = db.TestRuns.ToList(),
                    TestExecutions = db.TestExecutions.ToList()
                };
                ViewBag.ExecutionHeaders = typeof (TestExecution).GetProperties();
                ViewBag.RunHeaders = typeof (Run).GetProperties().Where(x => x.Name != "Executions");
                return View(viewModel);
            }
        }

        public ActionResult DropMenuAjax()
        {
            using (var db = new TestStatsContext())
            {
                _executions = db.TestExecutions.ToList();
                return View(_executions);
            }
        }

        [HttpPost]
        public ActionResult GetExecution(int id)
        {
            TestExecution execution;
            if (_executions == null)
            {
                using (var db = new TestStatsContext())
                {
                    execution = db.TestExecutions.First(x => x.ID == id);
                    return
                        Json(
                            new
                            {
                                execution.Fixture,
                                execution.Name,
                                execution.Description,
                                execution.Result,
                                ExecutionTimeInSecs = execution.ExecutionTime.TotalSeconds
                            });
                }
            }
            execution = _executions.First(x => x.ID == id);
            return
                Json(
                    new
                    {
                        execution.Fixture,
                        execution.Name,
                        execution.Description,
                        execution.Result,
                        ExecutionTimeInSecs = execution.ExecutionTime.TotalSeconds
                    });
        }
    }
}