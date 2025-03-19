using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Yildiz.Edu.WebUI.DataAccess.Abstract;
using Yildiz.Edu.WebUI.DataAccess.Context;
using Yildiz.Edu.WebUI.Entities;

namespace Yildiz.Edu.WebUI.Controllers
{
    public class FacultiesController(IFacultyDal facultyDal) : Controller
    {
        // GET: Faculties
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View(facultyDal.GetAll()));
        }

        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = facultyDal.GetAll().FirstOrDefault(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return await Task.Run(() => View(faculty));
        }

        // GET: Faculties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                facultyDal.Add(faculty);

                return RedirectToAction(nameof(Index));
            }
            return await Task.Run(() => View(faculty));
        }

        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var faculty = facultyDal.Get(id);
            return await Task.Run(() => View(faculty));
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyName,DeanName,EstablishedDate")] Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    facultyDal.Update(faculty);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return await Task.Run(() => View(faculty));
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = facultyDal.GetAll()
                .FirstOrDefault(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return await Task.Run(() => View(faculty));
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            facultyDal.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyExists(int id)
        {
            return facultyDal.Get(id)!=null!;
        }
    }
}
