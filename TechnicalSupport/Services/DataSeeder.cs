using TechnicalSupport.Data;
using TechnicalSupport.Models;

namespace TechnicalSupport.Services
{
    public class DataSeeder
    {
        public void Seed(MyDataContext context)
        {
            // Создаю несколько пользователей по умолчанию
            var users = new[]
            {
                new User
                {
                    Id = 1,
                    Login = "1",
                    Password = "1",
                    FirstName = "Вася",
                    LastName = "Пупкин",
                    Patronymic= "Андреевич",
                    Role = Role.User
                },
                new User
                {
                    Id = 2,
                    Login = "2",
                    Password = "2",
                    FirstName = "Тимур",
                    LastName = "Кашменских",
                    Patronymic= "Олегович",
                    Role = Role.Executor
                },
                new User
                {
                    Id = 3,
                    Login = "3",
                    Password = "3",
                    FirstName = "Асатбек",
                    LastName = "Рашидов",
                    Patronymic = "Асатбекович",
                    Role = Role.Executor
                },
                new User
                {
                    Id = 4,
                    Login = "admin",
                    Password = "admin",
                    FirstName = "Данис",
                    LastName = "Мингалеев",
                    Patronymic = "Ринатович",
                    Role = Role.Admin
                }
            };

            var requests = new[]
            {
                new Request
                {
                    Id = 1000001,
                    RegistrationDate = DateTime.Now,
                    ExecutorId = null,
                    UserId = 2,
                    IssueType = "device",
                    Priority = "средняя",
                    Title = "сломался принтер",
                    Description = "не включается, не реагирует.",
                    Status = RequestStatus.Created,
                    UnreadMessages = 5
                },
                new Request
                {
                    Id = 1000002,
                    RegistrationDate = DateTime.Now,
                    ExecutorId = 1,
                    UserId = 3,
                    IssueType = "device",
                    Priority = "высокая",
                    Title = "Не работает компьютер",
                    Description = "Нажимаю на кнопку на процессоре и ничего не происходит! Сделайте что-нибудь пожалуйста!",
                    Status = RequestStatus.InProgress,
                    UnreadMessages = 1
                },
                new Request
                {
                    Id = 1000003,
                    RegistrationDate = DateTime.Now,
                    ExecutorId = 2,
                    UserId = 4,
                    IssueType = "device",
                    Priority = "высокая",
                    Title = "Мышка не работает",
                    Description = "Двигаю мышкой и на экране ничего не двигается, не нажимается!",
                    Status = RequestStatus.InProgress,
                    UnreadMessages = 0
                },
                new Request
                {
                    Id = 1000004,
                    RegistrationDate = DateTime.Now,
                    ExecutorId = 1,
                    UserId = 4,
                    IssueType = "device",
                    Priority = "средння",
                    Title = "Зависла программа",
                    Description = "Программа зависла намертво",
                    Status = RequestStatus.Closed,
                    UnreadMessages = 0
                },
                new Request
                {
                    Id = 1000005,
                    RegistrationDate = DateTime.Now,
                    ExecutorId = 2,
                    UserId = 4,
                    IssueType = "device",
                    Priority = "средння",
                    Title = "Не работает кнопка Пуск",
                    Description = "Помогите! Не работает кнопка,невозможно работать!",
                    Status = RequestStatus.Closed,
                    UnreadMessages = 99
                },
            };

            context.Users.AddRange(users);
            context.Requests.AddRange(requests);

            // Сейв
            //context.SaveChanges();
        }
    }
}
