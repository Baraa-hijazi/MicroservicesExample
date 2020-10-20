using System.Threading.Tasks;
using IdentityService.Core.DTOs.Account;
using IdentityService.Services.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("Identity/[controller]")]
    [ApiController]
    public class AdministrationController : Controller
    {
        public IAdministrationServices AdministrationServices { get; }

        public AdministrationController(IAdministrationServices administrationServices)
        {
            AdministrationServices = administrationServices;
        }

        [HttpGet("Get-All")]
        public IActionResult Get() => Ok(AdministrationServices.Get());

        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> Get(string id) => Ok(await AdministrationServices.Get(id));

        [HttpPost("Create-Role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto) => Ok(await AdministrationServices.CreateRole(createRoleDto));

        [HttpPut("Edit-Role")]
        public async Task<IActionResult> Put(string id, [FromBody] CreateRoleDto createRoleDto) => Ok(await AdministrationServices.Put(id, createRoleDto));

        [HttpDelete("Delete-Role")]
        public async Task<IActionResult> Delete(string id) => Ok(await AdministrationServices.Delete(id));
    }
}