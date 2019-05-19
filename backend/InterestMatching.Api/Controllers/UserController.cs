﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterestMatching.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterestMatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("{id}", Name = "GetPair")]
        public User GetPair(string userMail)
        {
            var user = UserTable.Get(userMail);
            return user;
        }

        [HttpPost]
        public string NewUser([FromBody] User user)
        {
            UserTable.Add(user);
            return user.Email;
        }
    }
}
