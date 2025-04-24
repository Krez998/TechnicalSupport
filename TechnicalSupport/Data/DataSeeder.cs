using DataAccessLayer;
using DataAccessLayer.Models;
using Shared.Models.Ticket;
using Shared.Models.User;

namespace TechnicalSupport.Data
{
    public class DataSeeder
    {
        public void Seed(DataContext context)
        {
            var users = new[]
            {
                new User
                {
                    Username = "1",
                    PasswordHash = "1",
                    FirstName = "Вася",
                    LastName = "Пупкин",
                    Patronymic= "Андреевич",
                    Role = Role.User
                },
                new User
                {
                    Username = "2",
                    PasswordHash = "2",
                    FirstName = "Тимур",
                    LastName = "Кашменских",
                    Patronymic= "Олегович",
                    Role = Role.Agent
                },
                new User
                {
                    Username = "3",
                    PasswordHash = "3",
                    FirstName = "Асатбек",
                    LastName = "Рашидов",
                    Patronymic = "Асатбекович",
                    Role = Role.Agent
                },
                new User
                {
                    Username = "admin",
                    PasswordHash = "admin",
                    FirstName = "Данис",
                    LastName = "Мингалеев",
                    Patronymic = "Ринатович",
                    Role = Role.Admin
                }
            };


            var chat = new[]
{
                new Chat
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    IsClosed = false
                }
            };

            var tickets = new[]
            {
                new Ticket
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = null,
                    UserId = 1,
                    ChatId = 1,
                    IssueType = "device",
                    Priority = "средняя",
                    Title = "сломался принтер",
                    Description = "не включается, не реагирует.",
                    Status = TicketStatus.Created,
                    UnreadMessages = 5
                },
                new Ticket
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = 2,
                    UserId = 1,
                    IssueType = "device",
                    Priority = "высокая",
                    Title = "Не работает компьютер",
                    Description = "Нажимаю на кнопку на процессоре и ничего не происходит! Сделайте что-нибудь пожалуйста!",
                    Status = TicketStatus.InProgress,
                    UnreadMessages = 1
                },
                new Ticket
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = 2,
                    UserId = 1,
                    IssueType = "device",
                    Priority = "высокая",
                    Title = "Мышка не работает",
                    Description = "Двигаю мышкой и на экране ничего не двигается, не нажимается!",
                    Status = TicketStatus.InProgress,
                    UnreadMessages = 0
                },
                new Ticket
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = 2,
                    UserId = 1,
                    IssueType = "device",
                    Priority = "средння",
                    Title = "Зависла программа",
                    Description = "Программа зависла намертво",
                    Status = TicketStatus.Closed,
                    UnreadMessages = 0
                },
                new Ticket
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = 3,
                    UserId = 1,
                    IssueType = "device",
                    Priority = "средння",
                    Title = "Не работает кнопка Пуск",
                    Description = "Помогите! Не работает кнопка,невозможно работать!",
                    Status = TicketStatus.Closed,
                    UnreadMessages = 99
                },
            };

            var messages = new[]
            {
                new Message
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    ChatId = 1,
                    SenderId = 1,
                    Content = "Здравствуйте! У меня проблемы с принтером. Он не печатает."
                },
                new Message
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    ChatId = 1,
                    SenderId = 2,
                    Content = "Извините за неудобства. Могу я узнать, какая именно информация о принтере у вас есть? Модель и марка?",
                },
                new Message
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    ChatId = 1,
                    SenderId = 1,
                    Content = "У меня принтер HP LaserJet 1018. Он просто не реагирует.",
                },
                new Message
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    ChatId = 1,
                    SenderId = 2,
                    Content = "Спасибо за информацию. Можете ли вы сказать, какие действия вы уже предприняли? Пробовали перезагружать принтер или компьютер?" +
                    " Есть ли на принтере какие-либо сообщения об ошибках или индикаторы, которые могли бы указать на проблему.",
                },
                new Message
                {
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime(),
                    ChatId = 1,
                    SenderId = 1,
                    Content = "Да, я его перезагрузил, но ничего не изменилось. Я также проверил, подключен ли он к сети. На дисплее только мигает красный индикатор, и больше ничего.",
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
            context.Tickets.AddRange(tickets);
            context.SaveChanges();
            context.Chats.AddRange(chat);
            context.SaveChanges();
            context.Messages.AddRange(messages);
            context.SaveChanges();
            context.Dispose();
        }
    }
}
