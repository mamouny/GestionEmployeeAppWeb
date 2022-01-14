using GestionEmployeeAppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionEmployeeAppWeb.Controllers
{
    public class DepartementController : Controller
    {
        private readonly EmployeeContext _Db;

        public DepartementController(EmployeeContext Db)
        {
            _Db = Db;
        }
        public IActionResult Index()
        {
            try
            {
                var depList = from a in _Db.Departements
                              select new Departements
                              {
                                  DepId = a.DepId,
                                  DepartementName = a.DepartementName,
                              };
                return View(depList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult Create(Departements obj)
        {
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartement(Departements obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (obj.DepId == 0)
                    {
                        _Db.Departements.Add(obj);
                        await _Db.SaveChangesAsync();
                    }
                    else
                    {
                        _Db.Entry(obj).State = EntityState.Modified;
                        await _Db.SaveChangesAsync();

                    }

                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteDepartement(int id)
        {
            try
            {
                
                var dep = await _Db.Departements.FindAsync(id);
                if (dep != null)
                {
                    _Db.Departements.Remove(dep);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
