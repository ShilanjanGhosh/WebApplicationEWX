using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationEWX.Models;
using WebApplicationEWX.Repo;

namespace WebApplicationEWX.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeesRepo _empRepo;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeesRepo empRepo)
        {
            _logger = logger;
            _empRepo = empRepo;
        }
        // GET: EmployeeController
        public ActionResult Index(int pageSize = 5, int pageId = 1)
        {
            var emp = _empRepo.GetEmployees(pageSize, pageId).Result;
            ViewData["PageId"] = pageId;
            ViewData["PageCount"] = (int)Math.Ceiling((double)_empRepo.GetEmployeeCount().Result / pageSize);
            ViewData["PageSize"] = pageSize;

            return View(emp);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var emp = _empRepo.GetEmployee(id).Result;
            if (emp == null)
                return BadRequest("Invalid Employee Id");
            return View(emp);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "FirstName,LastName,Email")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empRepo.CreateEmployee(employee).Wait();
                    return RedirectToAction(nameof(Index));
                }                
            }
            catch
            {
                
            }
            return View(employee);
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var emp = _empRepo.GetEmployee(id).Result;
            if (emp == null)
                return BadRequest("Invalid Employee Id");
            return View(emp);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "Id,FirstName,LastName,Email")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empRepo.EditEmployee(employee).Wait();
                    return RedirectToAction(nameof(Index));
                }                    
            }
            catch
            {
                
            }
            return View(employee);
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var emp = _empRepo.GetEmployee(id).Result;
            if (emp == null)
                return BadRequest("Invalid Employee Id");
            return View(emp);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(include: "Id")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empRepo.Delete(employee.Id).Wait();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {

            }
            return View(employee);
        }
    }
}
