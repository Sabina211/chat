using Chat.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected UserInfo CurrentUser => new(HttpContext.User);
    /*
     * {"data": result, "error": "null"}
     */
}