using System.Linq;
using InterestMatching.Engine;
using Microsoft.AspNetCore.Mvc;

namespace InterestMatching.Api.Controllers
{
    [Route("api/Other")]
    [ApiController]
    public class OtherController : Controller
    {
        [HttpGet]
        public string WeatherForecasts()
        {
            User first;
            using(var connector = new BDConnector())
                first = connector.Get<User>("UserIfo").First();

            return $"{first.Name} + {first.Age}";
        }
    }
}
