using Microsoft.AspNetCore.Mvc;

namespace Faly.Api.Controllers;

[Route("/")]
public class HomeController : CustomControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Index()
    {
        return Redirect("api/doc");
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("api/doc")]
    public IActionResult ApiDocumentation()
    {
        return Content("<!doctype html>\n <html lang=\"en\">\n <head>\n<meta charset=\"utf-8\">\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\">\n<title>HomeWork</title>\n<link rel=\"icon\" type=\"image/x-icon\" href=\"/notifi-icon.png\">\n<link rel=\"stylesheet\" href=\"/doc/stoplight.min.css\">\n<script src=\"/doc/stoplight.min.js\"></script>\n </head>\n <body>\n<elements-api apiDescriptionUrl=\"/swagger/v1/swagger.json\n\" router=\"hash\" data-theme='dark' />\n </body>\n <script src=\"/doc/jquery.min.js\"></script>\n <script src=\"/doc/sl-search.js\"></script>\n </html>\n    ", "text/html");
    }

}

