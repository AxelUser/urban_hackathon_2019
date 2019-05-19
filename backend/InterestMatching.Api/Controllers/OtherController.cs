﻿using System;
using System.Linq;
using InterestMatching.Engine;
using Microsoft.AspNetCore.Mvc;

namespace InterestMatching.Api.Controllers
{
    [Route("api/SetUser")]
    [ApiController]
    public class SetUserController : Controller
    {
        [HttpGet]
        public string SetUser()
        {
            
            var first = new User()
            {
                Age = 12,
                Description = "Test User",
                Email = "test@mail.test",
                Interests = new[] {1, 2, 3, 4},
                Name = "John",
                SurName = "Dow",
                UserStats = new UserStats()
                {
                    GoodMood = 2,
                    NonPunctuality = 3,
                    SelfCentered = 9
                }
            };
            

            UserTable.Add("test@mail.test", first);

            return "OK";
        }
    }

    [Route("api/GetUser")]
    [ApiController]
    public class GetUserController : Controller
    {
        [HttpGet]
        public string GetUser()
        {
            var first = UserTable.Get("test@mail.test");
            if (first == null) return "User does not exist";

            return $"{first.Name} + {first.UserStats.GoodMood} + {first.UserStats.Sociability} + {first.Age}";
        }
    }
}
