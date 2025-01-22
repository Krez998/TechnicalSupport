using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnicalSupport.Data;
using TechnicalSupport.Models.DTO;
using TechnicalSupport.Models.Response;

namespace TechnicalSupport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public MyDataContext _dataContext;

        public AuthController(MyDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<ActionResult> Auth([FromBody] LoginDTO auth)
        {
            var user = _dataContext.Users.Where(x => x.Login == auth.Username).FirstOrDefault();

            if (user != null)
            {
                if (user.Password == auth.Password)
                {
                    
                    return Ok(new UserDataResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Patronymic = user.Patronymic,
                        UserType = user.UserType
                    });
                }
                else
                    return StatusCode(403, "Неверный пароль");
            }
            else
            {
                return StatusCode(403, "Не найден пользователь");
            }
        }
    }
}
