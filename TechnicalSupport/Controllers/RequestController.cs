using Microsoft.AspNetCore.Mvc;
using TechnicalSupport.Data;
using TechnicalSupport.Models;
using TechnicalSupport.Models.DTO;
using TechnicalSupport.Models.Response;

namespace TechnicalSupport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        public MyDataContext _dataContext;

        public RequestController(MyDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task CreateRequest([FromBody] CreateRequestDTO createRequestModel)
        {
            var lastRequest = _dataContext.Requests.LastOrDefault();
            int newId = lastRequest is null ? 10000 : lastRequest.Id + 1;

            _dataContext.Requests.Add(new Request
            {
                Id = newId,
                RegistrationDate = DateTime.Now,
                ExecutorId = 1,
                UserId = createRequestModel.UserId,
                IssueType = createRequestModel.IssueType,
                Priority = createRequestModel.Priority,
                Title = createRequestModel.Title,
                Description = createRequestModel.Description,
                Status = (RequestStatus)createRequestModel.Status
            });
        }

        /// <summary>
        /// Изменение статуса заявки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult> ChangeRequestStatus(int id, int status)
        {
            var request = _dataContext.Requests.Where(r => r.Id == id).FirstOrDefault();
            RequestResponse response = new RequestResponse();

            if (request != null)
            {
                request.Status = (RequestStatus)status;

                response = new RequestResponse
                {
                    Id = request.Id,
                    UserId = request.UserId,
                    IssueType = request.IssueType,
                    Priority = request.Priority,
                    Title = request.Title,
                    Description = request.Description,
                    Status = (RequestStatus)status,
                    UnreadMessages = request.UnreadMessages
                };
                return StatusCode(200, response);
            }
            else
            {
                return StatusCode(204);
            }     
        }

        [HttpPatch("executor/{id}")]
        public IActionResult SetRequestExecutor(int id, [FromQuery] int executorId)
        {
            var request = _dataContext.Requests.Where(r => r.Id == id).FirstOrDefault();

            if (request != null)
            {
                var executor = _dataContext.Users.Where(e => e.Id == executorId).FirstOrDefault();
                if (executor != null)
                {
                    request.ExecutorId = executor.Id;
                    request.Status = RequestStatus.Assigned;
                    return Ok();
                }
                else
                    return StatusCode(204, "такого пользователя не существует"); 
            }
            else
                return StatusCode(204, "такой заявки не существует");
        }

        /// <summary>
        /// Получение заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRequest(int id)
        {
            RequestResponse response = new RequestResponse();

            var request = _dataContext.Requests.FirstOrDefault(x => x.Id == id);

            var executorFullName = _dataContext.Users
                .Where(u => u.Id == request?.ExecutorId)
                .Select(x => new { FullName = $"{x.LastName} {x.FirstName} {x.Patronymic}".Trim() })
                .FirstOrDefault()?.FullName;

            if (request != null)
            {
                response = new RequestResponse
                {
                    Id = request.Id,
                    ExecutorFullName = executorFullName,
                    ExecutorId = request.ExecutorId,
                    UserId = request.UserId,
                    IssueType = request.IssueType,
                    Priority = request.Priority,
                    Title = request.Title,
                    Description = request.Description,
                    Status = request.Status,
                    UnreadMessages = request.UnreadMessages
                };

                return StatusCode(200, response);
            }
            else
            {
                return StatusCode(204);
            }
        }

        /// <summary>
        /// Получение всех заявок
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Request> GetAll([FromQuery] GetRequestsDTO request)
        {
            var requests = request.Status != null ?
                _dataContext.Requests.Where(r => r.Status != request.Status).ToList()
                : _dataContext.Requests;

            if (request.IsShowNotAssigned)
                requests = requests.Where(r => r.ExecutorId == null).ToList();

            return requests;
        }


        /// <summary>
        /// (Пока не используется) Удаление заявки
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var model = _dataContext.Requests.FirstOrDefault(x => x.Id == id);
            _dataContext.Requests.Remove(model);
        }
    }
}
