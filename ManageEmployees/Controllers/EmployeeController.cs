using ManageEmployees.Entities;
using ManageEmployees.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployees.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var employees = _unitOfWork.Employees.GetAll();
            return View(employees);
        }
        public IActionResult Search(string keyword)
        {
            if (keyword != null)
            {
                var employees = _unitOfWork.Employees.Find(x => x.Name.Contains(keyword));
                return View("Index", employees);
            }
            else
            {
                var employees = _unitOfWork.Employees.GetAll();
                return View("Index", employees);
            }
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Employees.Add(employee);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _unitOfWork.Employees.GetById(id);
            _unitOfWork.Employees.Remove(employee);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
