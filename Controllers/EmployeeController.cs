using Azure.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPiCaching.Models;
using WebAPiCaching.Services;

namespace WebAPiCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService<Employee, int> _employeeService;
        ICacheService _cacheService;
        public EmployeeController(IEmployeeService<Employee, int> employeeService, ICacheService cacheService = null)
        {
            _employeeService = employeeService;
            _cacheService = cacheService;
        }

        [HttpGet("ListAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var cachedata = _cacheService.GetData<IEnumerable<Employee>>("employees");

            if (cachedata != null && cachedata.Count() > 0) {
                Console.WriteLine("Data From cache");
                return Ok(cachedata);
            }

            cachedata =await _employeeService.GetEmployees();

            if (cachedata != null)
            {
                Console.WriteLine("Data From Database");
                var expirTime = DateTimeOffset.Now.AddSeconds(30);
                _cacheService.SetData<IEnumerable<Employee>>("employees",cachedata, expirTime);
                return Ok(cachedata);
            }
            return BadRequest(cachedata);
        }

        [HttpGet("GetEmploeeById/{EmployeeId}")]
        public async Task<IActionResult> GetEmployeeById(int EmployeeId) { 
            if (EmployeeId < 0) {
                return BadRequest("Invalid Id");
            }
            var model = await _employeeService.GetEmployeeById(EmployeeId);
            if (model != null)
            {
                return Ok(model);
            }
            return NoContent();
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(Employee entity) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _isAdded = await _employeeService.AddEmployee(entity);
            if (_isAdded) {
                return Ok(entity);
            }
            return BadRequest();
        }

        [HttpDelete("RemoveEmployee/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId) {
            if (employeeId < 0) {
                return BadRequest("Invalid EmploeeId");
            }
            var _isDeleted = await _employeeService.DeleteEmployee(employeeId);
            if (_isDeleted)
            {
                return Ok($"Employee with Id {employeeId} Removed Succesfully");
            }
            else { 
                return BadRequest("Invalid EmploeeId");
            }
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> DeleteEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = await _employeeService.UpdateEmployee(employee);
            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("Invalid Request");
            }
        }
    }
}
