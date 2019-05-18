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
            var connector = new Engine.BDConnector();

            return connector.Get("select * from TestTable");
        }
    }
}
