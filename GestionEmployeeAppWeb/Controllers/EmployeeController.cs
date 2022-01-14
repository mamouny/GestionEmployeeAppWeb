using GestionEmployeeAppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionEmployeeAppWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _Db;

        public EmployeeController(EmployeeContext Db)
        {
            _Db = Db;
        }
        public IActionResult EmployeeList()
        {
            try
            {
                //var empList = _Db.Employees.ToList();
                var empList = from a in _Db.Employees
                              join b in _Db.Departements
                              on a.DepId equals b.DepId
                              into Dep
                              from b in Dep.DefaultIfEmpty()

                              select new Employee
                              {
                                  EmpId=a.EmpId,
                                  EmpNom=a.EmpNom,
                                  EmpPrenom=a.EmpPrenom,
                                  Age=a.Age,
                                  DepId=a.DepId,

                                  DepartementName=b==null? "":b.DepartementName
                              };
                return View(empList);
            }
            catch(Exception ex)
            {
                return View();
            }
            
        }

        public IActionResult Create(Employee obj)
        {
            loadDDL();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployees(Employee obj)
        {
            try {
                if (ModelState.IsValid)
                {
                    if (obj.EmpId == 0)
                    {
                        _Db.Employees.Add(obj);
                        await _Db.SaveChangesAsync();
                    }
                    else
                    {
                        _Db.Entry(obj).State = EntityState.Modified;
                        await _Db.SaveChangesAsync();

                    }

                    return RedirectToAction("EmployeeList");
                }
                return View();
            }
            catch(Exception ex) {
                return RedirectToAction("EmployeeList");
            }
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var emp =await  _Db.Employees.FindAsync(id);
                if (emp!=null)
                {
                    _Db.Employees.Remove(emp);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("EmployeeList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("EmployeeList");
            }
        }
        private void loadDDL()
        {
            try
            {
                List<Departements> depList = new List<Departements>();
                depList = _Db.Departements.ToList();
                depList.Insert(0, new Departements { DepId = 0, DepartementName = "Please select" });
                ViewBag.DepList = depList;
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
