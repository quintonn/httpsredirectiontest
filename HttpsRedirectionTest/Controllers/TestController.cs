using Microsoft.AspNetCore.Mvc;

namespace HttpsRedirectionTest.Controllers
{
    [Route("basic")]
    public class TestController
    {
        [Route("test1")]
        public IActionResult Test1()
        {
            return new OkObjectResult("http method -> Ok, it works");
        }

        [Route("test2")]
        [RequireHttps]  // I expect when I browse to http://localhost:5020/basic/test2 to be sent to https://localhost:5021/basic/test2
        public IActionResult Test2()
        {
            return new OkObjectResult("httpS method -> Ok, it works");
        }
    }
}
