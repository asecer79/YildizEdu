using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        readonly IDepartmentService _departmentService;
        private IFacultyService _facultyService;

        public DepartmentsController(IDepartmentService departmentService, IFacultyService facultyService)
        {
            _departmentService = departmentService;
            _facultyService = facultyService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return await Task.FromResult<IActionResult>(Ok(_departmentService.GetAll()));
        }


        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _departmentService.Get(p => p.Id == id.Value);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Department department)
        {
            _departmentService.Add(department);

            return await Task.FromResult<IActionResult>(Ok(department));
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] Department department)
        {
            _departmentService.Update(department);
            return await Task.FromResult<IActionResult>(Ok(department));
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] Department department)
        {
            if (department != null)
            {
                _departmentService.Delete(department);
            }

            return await Task.FromResult<IActionResult>(Ok(department));
        }

        private bool DepartmentExists(int id)
        {
            return _departmentService.GetAll().Any(e => e.Id == id);
        }
    }
}
