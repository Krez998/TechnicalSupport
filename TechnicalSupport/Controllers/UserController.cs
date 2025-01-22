using Microsoft.AspNetCore.Mvc;
using TechnicalSupport.Data;
using TechnicalSupport.Models;
using TechnicalSupport.Models.DTO;
using TechnicalSupport.Models.Response;

namespace TechnicalSupport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public MyDataContext _dataContext;

        public UserController(MyDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// (Не используется во fron-end)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return _dataContext.Users.ToList();
        }

        /// <summary>
        /// Получение списка всех исполнителей
        /// </summary>
        /// <returns></returns>
        [HttpGet("allExecutors")]
        public IEnumerable<UserResponse> GetAllExecutors()
        {
            var users = _dataContext.Users.Where(u => u.UserType == UserType.Executor).ToList();
            var executors = new List<UserResponse>();
            foreach (var user in users)
            {
                executors.Add(new UserResponse()
                {
                    Id = user.Id,
                    FirstName = user.FirstName, 
                    LastName = user.LastName, 
                    Patronymic = user.Patronymic
                });
            }

            return executors;
        }

        /// <summary>
        /// Создание нового пользователя в системе
        /// </summary>
        /// <param name="createUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            var lastId = _dataContext.Users.LastOrDefault();
            int newId = lastId is null ? 0 : lastId.Id + 1;

            _dataContext.Users.Add(new User
            {
                Id = newId,
                Login = createUserDTO.Login,
                Password = createUserDTO.Password,
                UserType = createUserDTO.UserType,
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                Patronymic= createUserDTO.Patronymic,
            });
        }
    }
}
