using TechnicalSupport.Models;

namespace TechnicalSupport.Data
{
    public class MyDataContext
    {
        public List<Request> Requests { get; set; }
        public List<User> Users { get; set; }

        public MyDataContext()
        {
            Requests = new List<Request>();
            Users = new List<User>();
        }
    }
}
