using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.WebUI.Controllers
{
    public class DepartmentsController : Controller
    {
        
        IDepartmentDal _departmentDal;
        private IFacultyDal _facultyDal;

        public DepartmentsController(IDepartmentDal departmentDal, IFacultyDal facultyDal)
        {
            _departmentDal = departmentDal;
            _facultyDal = facultyDal;
        }

        // GET: Departments
        public  async Task<IActionResult> Index()
        {

            
            return await Task.FromResult<IActionResult>(View(_departmentDal.GetAll()));
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _departmentDal.Get(id.Value);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_facultyDal.GetAll(), "Id", "FacultyName");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentName,FacultyId,HeadOfDepartment")] Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentDal.Add(department);

                return await Task.FromResult<IActionResult>(RedirectToAction(nameof(Index)));
            }
            ViewData["FacultyId"] = new SelectList(_facultyDal.GetAll(), "Id", "FacultyName", department.FacultyId);
            return await Task.FromResult<IActionResult>(View(department));
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }

            var department = _departmentDal.Get(id.Value);

            if (department == null)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }
            ViewData["FacultyId"] = new SelectList(_facultyDal.GetAll() , "Id", "FacultyName", department.FacultyId);
            return await Task.FromResult<IActionResult>(View(department));
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName,FacultyId,HeadOfDepartment")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentDal.Update(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            ViewData["FacultyId"] = new SelectList(_facultyDal.GetAll(), "Id", "FacultyName", department.FacultyId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return  await Task.FromResult<IActionResult>(NotFound());
            }

            var department = _departmentDal.Get(id.Value);
            if (department == null)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }

            return await Task.FromResult<IActionResult>(View(department));
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = _departmentDal.Get(id);

            if (department != null)
            {
                _departmentDal.Delete(id);
            }

            return await Task.FromResult<IActionResult>(RedirectToAction(nameof(Index)));
        }

        private bool DepartmentExists(int id)
        {
            return _departmentDal.GetAll().Any(e => e.Id == id);
        }
    }
}
