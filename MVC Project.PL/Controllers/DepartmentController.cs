using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.BLL.Interfaces;
using MVC_Project.DAL.Models;
using System;
using System.Threading.Tasks;

namespace MVC_Project.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async  Task<IActionResult> Index()
        {


            var departments =await _unitOfWork.DepartmentRepository.GetALL();
            return View(departments);
        }
        
        public IActionResult Create() 
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {

                if (ModelState.IsValid)
                {
                    await _unitOfWork.DepartmentRepository.Add(department);
                    int count =await _unitOfWork.Complete();
                   if(count>0)
                       TempData["message"] = "Department is Created successfully";
                   return RedirectToAction(nameof(Index));
                }
            

            return View(department);
        }
        
        public async Task<IActionResult> Details(int? id, string ViewName = "Details") 
        {
            if (id is null)
                return BadRequest();
            var department =await _unitOfWork.DepartmentRepository.Get(id.Value);
            if(department is null)
                return NotFound();  
            return View(ViewName,department);  
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id,"Edit");
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if(id !=department.Id)
                return BadRequest();        
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    await _unitOfWork.Complete(); 
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
           
            return View(department);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id,Department department)
        {
            if (id != department.Id)
                return BadRequest();
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentRepository.Delete(department);
                    await _unitOfWork.Complete(); 
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(department);
        }



    }
}
