using MvcMovie1.Models;

namespace MvcMovie1.BaseDeDatosFicticia
{
    public class FakeUserDB
    {
        public static List<LoginModel> Users = new List<LoginModel>
        {
            new LoginModel
            {
                Id=1,
                User="admin",
                Password="1234",
            },
            new LoginModel
            {
                Id=2,
                User="1234",
                Password="admin"
            }
        };
    }
}
