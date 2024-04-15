using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using ReactCrudAPI.CommonFunctions;
using ReactCrudAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthonticationController : ControllerBase
    {

        private readonly EmployeeContext _employeeContext;
        private readonly IConfiguration _configuration;
        public AuthonticationController(EmployeeContext employeeContext, IConfiguration configuration)
        {
            _employeeContext = employeeContext;
            _configuration = configuration;

        }
        [HttpPost]
        public async Task<IActionResult> PostLoginDetails(UserDetail userModel)
        {
            try
            {
                if (userModel != null)
                {
                    var result = _employeeContext.userModels.Where(t => t.Email == userModel.Email && t.Password == userModel.Password).FirstOrDefault();
                    if (string.IsNullOrEmpty(result.Email))
                    {
                        return BadRequest("Invalide Credntials");
                    }
                    else
                    {
                        userModel.UserMessage = "Login Success";
                        userModel.AccessToken = GenerateToken.GetToken(userModel, _configuration);
                        return Ok(userModel);
                    }
                }
                else
                {
                    return BadRequest("No data found");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
