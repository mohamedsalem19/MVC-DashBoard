using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.BLL.Interfaces;
using MVC_Project.BLL.Repositories;
using MVC_Project.DAL.Models;
using MVC_Project.PL.Helpers;
using MVC_Project.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Project.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _EmployeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper )
        {
            //_EmployeeRepository = EmployeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchValue))
                employees = await _unitOfWork.EmployeeRepository.GetALL();
            else
                employees = _unitOfWork.EmployeeRepository.SearchEmployeesByName(SearchValue);

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmp);

            

        }

        public IActionResult Create()
        {
            //ViewBag.Departments = _departmentRepository.GetALL();
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {

            if (ModelState.IsValid)
            {
				employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");

				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                
                await _unitOfWork.EmployeeRepository.Add(mappedEmp);
                int count = await _unitOfWork.Complete();
                if (count > 0)
                    TempData["message"] = "Employee is Created successfully";
                

				return RedirectToAction(nameof(Index));
            }


            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null)
                return NotFound();

            var mappedEmp = _mapper.Map< Employee,EmployeeViewModel> (employee);


            return View(ViewName, mappedEmp);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM  )
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(employeeVM);
        }

        public async Task<IActionResult>  Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {

                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                     _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                    
                   int count=await _unitOfWork.Complete();
                    if (count > 0)
                        DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(employeeVM);
        }



    }
}
