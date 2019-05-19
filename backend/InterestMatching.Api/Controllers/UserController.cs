using System;
using System.Linq;
using InterestMatching.Engine;
using Microsoft.AspNetCore.Mvc;

namespace InterestMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("{id}", Name = "GetPair")]
        public Meeting GetPair(string userMail)
        {
            var user = UserTable.Get(userMail);
            var all = UserTable.GetAll()
                .Where(u => !u.Email.Equals(user.Email, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            var result = MatchingEngine.GetPair(user, all);

            return new Meeting
            {
                Date = DateTime.Today,
                FirstUserId = userMail,
                SecondUserId = result.Item2
            };
        }

        [HttpPost]
        public string NewUser([FromBody] User user)
        {
            UserTable.Add(user);
            return user.Email;
        }
    }
}
