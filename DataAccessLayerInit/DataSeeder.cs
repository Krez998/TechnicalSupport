using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Shared.Models.Ticket;
using Shared.Models.User;
using System.Security.Cryptography;

namespace TechnicalSupport.Data
{

    /// <summary>
    /// 
    /// </summary>
    public class PasswordHasher
    {
        /// <summary>
        /// Генерирует случайную соль и хеш.
        /// </summary>
        /// <param name="password">Пароль.</param>
        /// <returns></returns>
        public (string Hash, string Salt) HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10_000,
                numBytesRequested: 32
            );

            return (
                Hash: Convert.ToBase64String(hash),
                Salt: Convert.ToBase64String(salt)
            );
        }
    }

    public class DataSeeder
    {
        private readonly PasswordHasher _passwordHasher;

        public DataSeeder()
        {
            _passwordHasher = new PasswordHasher();
        }

        public void Seed(DataContext context)
        {
            // Проверяет, есть ли уже данные в базе
            if (context.Users.Any() || context.Tickets.Any())
                return;

            var user1Hash = _passwordHasher.HashPassword("1");
            var user2Hash = _passwordHasher.HashPassword("2");
            var agent1Hash = _passwordHasher.HashPassword("3");
            var agent2Hash = _passwordHasher.HashPassword("4");
            var adminHash = _passwordHasher.HashPassword("admin");

            var users = new[]
            {
                new User
                {
                    Username = "1",
                    PasswordHash = user1Hash.Hash,
                    PasswordSalt = user1Hash.Salt,
                    /// Либо только через BCrypt
                    /// PasswordHash = BCrypt.Net.BCrypt.HashPassword("1"),
                    /// PasswordSalt = "", // Пустая строка или null, так как не используется
                    FirstName = "Кирилл",
                    LastName = "Овчинников",
                    Patronymic= "Владимирович",
                    Role = Role.User
                },
                new User
                {
                    Username = "2",
                    PasswordHash = user2Hash.Hash,
                    PasswordSalt = user2Hash.Salt,
                    FirstName = "Анна",
                    LastName = "Петрова",
                    Patronymic= "Данииловна",
                    Role = Role.User
                },
                new User
                {
                    Username = "3",
                    PasswordHash = agent1Hash.Hash,
                    PasswordSalt = agent1Hash.Salt,
                    FirstName = "Тимур",
                    LastName = "Кашменских",
                    Patronymic= "Олегович",
                    Role = Role.Agent
                },
                new User
                {
                    Username = "4",
                    PasswordHash = agent2Hash.Hash,
                    PasswordSalt = agent2Hash.Salt,
                    FirstName = "Иван",
                    LastName = "Васильев",
                    Patronymic = "Павлович",
                    Role = Role.Agent
                },
                new User
                {
                    Username = "admin",
                    PasswordHash = adminHash.Hash,
                    PasswordSalt = adminHash.Salt,
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
                    Id = 5276840,
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = null,
                    CreatedById = 1,
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
                    Id = 5276841,
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = 2,
                    CreatedById = 1,
                    IssueType = "device",
                    Priority = "высокая",
                    Title = "Не работает компьютер",
                    Description = "Нажимаю на кнопку на процессоре и ничего не происходит! Сделайте что-нибудь пожалуйста!",
                    Status = TicketStatus.InProgress,
                    UnreadMessages = 1
                },
                new Ticket
                {
                    Id = 5276842,
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = 2,
                    CreatedById = 1,
                    IssueType = "device",
                    Priority = "высокая",
                    Title = "Мышка не работает",
                    Description = "Двигаю мышкой и на экране ничего не двигается, не нажимается!",
                    Status = TicketStatus.InProgress,
                    UnreadMessages = 0
                },
                new Ticket
                {
                    Id = 5276843,
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = 2,
                    CreatedById = 1,
                    IssueType = "device",
                    Priority = "средння",
                    Title = "Зависла программа",
                    Description = "Программа зависла намертво",
                    Status = TicketStatus.Closed,
                    UnreadMessages = 0
                },
                new Ticket
                {
                    Id = 5276844,
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    //AgentId = 3,
                    CreatedById = 1,
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

            context.Chats.AddRange(chat);
            context.SaveChanges();

            context.Tickets.AddRange(tickets);
            context.SaveChanges();

            context.Messages.AddRange(messages);
            context.SaveChanges();
        }
    }
}
