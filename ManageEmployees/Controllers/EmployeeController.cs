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
            return View();
        }
        public IActionResult GetAll()
        {
            var employees = _unitOfWork.Employees.GetAll();
            return Json(new {
                draw= 1,
                recordsTotal= employees.Count(),
                data =employees });
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
            try
            {
                _unitOfWork.Employees.Add(employee);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }
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
